using System;
using System.Windows.Forms;
using CQRSDemo.Domain.Account;
using CQRSDemo.Domain.Data.Repository;
using EventStore.ClientAPI;

namespace CQRSDemo.Forms
{
    public partial class TestForm : Form
    {
        private readonly EmployeeStore _accountRepo;
        private Guid _workingEmployee;

        public TestForm(IEventStoreConnection connection)
        {
            InitializeComponent();
            
            _accountRepo = new EmployeeStore(connection);
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;

            var employee = Employee.Create(firstName, lastName);
            _workingEmployee = employee.Id;

            await _accountRepo.Save(employee);
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            var employee = await _accountRepo.GetById(_workingEmployee, true);
            employee.Activate();

            await _accountRepo.Save(employee);
        }

        private async void btnDeactivate_Click(object sender, EventArgs e)
        {
            var employee = await _accountRepo.GetById(_workingEmployee, true);
            employee.Deactivate();
            await _accountRepo.Save(employee);
        }
    }
}
