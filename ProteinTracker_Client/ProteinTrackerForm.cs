using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProteinTracker_Client.ProteinTrackerService;

namespace ProteinTracker_Client
{
    public partial class ProteinTrackerForm : Form
    {
        private ProteinTrackingServiceSoapClient service = new ProteinTrackingServiceSoapClient();

        // its an array because its de-serialize as an array by default.
        private User[] _users;
        public ProteinTrackerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _users = service.ListUsers();

            cboSelectUser.DataSource = _users;
            cboSelectUser.DisplayMember = "UserName";
            cboSelectUser.ValueMember = "UserId";
        }




        private void tbxAddProtein_TextChanged(object sender, EventArgs e)
        {
          
        }

        // Add user
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int goal = 0;
            if (!String.IsNullOrEmpty(tbxAddName.Text) && int.TryParse(tbxAddGoal.Text, out goal))
            {
                service.AddUser(tbxAddName.Text, goal);
                _users = service.ListUsers();
                cboSelectUser.DataSource = _users;
                MessageBox.Show("User successfully added!");
                tbxAddName.Text = "";
                tbxAddGoal.Text = "";

            }
            else
            {
                MessageBox.Show("User not added, check fields are filled out correctly!");
            }

        }

        private void cboSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblGoal.Text = _users[cboSelectUser.SelectedIndex].Goal.ToString();
            lblTotal.Text = _users[cboSelectUser.SelectedIndex].Total.ToString();
        }

        private void btnAddProtein_Click(object sender, EventArgs e)
        {
            try
            {
                var auth = new AuthenticationHeader();
                auth.UserName = "Mark";
                auth.Password = "pass";

                int userId = _users[cboSelectUser.SelectedIndex].UserId;
                int amount = 0;
                
                if (int.TryParse(tbxAddProtein.Text, out amount))
                {
                  int  newTotal = service.AddProtein(auth, userId, amount);
                    _users[cboSelectUser.SelectedIndex].Total = newTotal;
                    lblTotal.Text = newTotal.ToString();
                }
            }
            catch (FaultException ex)
            {
                // FaultException is used to catch soap exceptions 
            }
        }
    }
}
