﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.DateTimes;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.Assignments;

namespace OtripleS.Web.Api.Services.Assignments
{
    public partial class AssignmentService : IAssignmentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public AssignmentService(IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<Assignment> CreateAssignmentAsync(Assignment assignment) =>
        TryCatch(async () =>
        {
            ValidateAssignmentOnCreate(assignment);

            return await this.storageBroker.InsertAssignmentAsync(assignment);
        });

        public async ValueTask<Assignment> ModifyAssignmentAsync(Assignment assignment)
        {
            Assignment maybeAssignment = await this.storageBroker.SelectAssignmentByIdAsync(assignment.Id);

            DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTime();

            return await this.storageBroker.UpdateAssignmentAsync(assignment);
        }
    }
}
