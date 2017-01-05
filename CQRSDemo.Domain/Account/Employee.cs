using System;
using System.Collections.Generic;
using System.Linq;
using DAS.Infrastructure.Practices.EventSourcing;

namespace CQRSDemo.Domain.Account
{
    public class Employee : Aggregate
    {
        private string _firstName;
        private string _lastName;
        private DateTimeOffset? _activationDate;
        private DateTimeOffset? _deactivationDate;
        
        private Employee()
        {
        }

        public static Employee Create(string firstName, string lastName)
        {
            var newId = Guid.NewGuid();
            var emp = new Employee();
            emp.ApplyEvent(new CreateEmployee(newId, firstName, lastName));

            return emp;
        }
        
        public void Activate()
        {
            if(_activationDate != null)
                throw new InvalidOperationException("Employee already activated");

            ApplyEvent(new ActivateEmployee(Id, DateTimeOffset.UtcNow));
        }

        public void Deactivate()
        {
            if(_activationDate == null || _deactivationDate != null)
                throw new InvalidOperationException("Employee is already deactivated");

            ApplyEvent(new DeactivateEmployee(Id, DateTimeOffset.UtcNow));
        }

        #region -- Event Applicators ---

        private void Apply(CreateEmployee employee)
        {
            Id = employee.EntityId;

            _lastName = employee.LastName;
            _firstName = employee.FirstName;
        }

        private void Apply(ActivateEmployee e)
        {
            _deactivationDate = null;
            _activationDate = e.ActivationDate;
        }

        private void Apply(DeactivateEmployee e)
        {
            _activationDate = null;
            _deactivationDate = e.DeactivationDate;
        }

        private void Apply(EmployeeSnapshot e)
        {
            _firstName = e.FirstName;
            _lastName = e.LastName;
            _activationDate = e.ActivationDate;
            _deactivationDate = e.DeactivationDate;
        }

        #endregion

        public override EventInfo GetSnapshot()
        {
            return new EmployeeSnapshot
            {
                FirstName = _firstName,
                LastName = _lastName,
                ActivationDate = _activationDate,
                DeactivationDate = _deactivationDate
            };
        }
    }
}
