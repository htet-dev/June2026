using June2026.Database.AppDbContextModels;
using Microsoft.VisualBasic.ApplicationServices;

namespace June2026.WinFormsApp2
{
    public partial class FrmUser : Form
    {
        private readonly AppDbContext _db;
        private int _editUserId = 0;

        public FrmUser()
        {
            InitializeComponent();
            _db = new AppDbContext();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            var lst = _db.TblUsers.ToList();

            int RowNo = 0;
            List<UserDto> users = new List<UserDto>();
            foreach (var item in lst)
            {
                UserDto user = new UserDto()
                {
                    Username = item.Username,
                    UserId = item.UserId,
                    Password = item.Password,
                    RowNo = ++RowNo
                };

                users.Add(user);
            }

            dgvData.DataSource = users;
            ClearControls();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        public class UserDto
        {
            public int RowNo { get; set; }
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private void ClearControls()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_editUserId == 0)   // add new username and password
            {
                _db.TblUsers.Add(new TblUser
                {
                    Password = txtPassword.Text.Trim(),    // Trim() is Left Trim and Right Trim
                    Username = txtUsername.Text.Trim()
                });               
            }
            else   // update existing username and password
            {
                var item = _db.TblUsers
                                    .Where(x => x.UserId == _editUserId)
                                    .FirstOrDefault();

                if (item is null)
                {
                    MessageBox.Show(
                        "User not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                item.Username = txtUsername.Text.Trim();
                item.Password = txtPassword.Text.Trim();                
            }

            _db.SaveChanges();
            _editUserId = 0;

            BindData();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == -1) return; 
            if(e.ColumnIndex == 0)   // edit button
            {
                int userId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells[nameof(colUserId)].Value);
                var item = _db.TblUsers
                                .Where(x => x.UserId == userId)
                                .FirstOrDefault();

                if (item is null)
                {
                    Console.WriteLine("UserId Not Found.");
                    return;
                }

                txtUsername.Text = item.Username;
                txtPassword.Text = item.Password;
                _editUserId = item.UserId;

            }
            else if (e.ColumnIndex == 1) // delete button
            {
                var result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    int userId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells[nameof(colUserId)].Value);
                    var  item = _db.TblUsers
                                    .Where(x => x.UserId == userId)
                                    .FirstOrDefault();

                    if (item is null)
                    {
                        MessageBox.Show(
                            "User not found.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }                        

                    _db.Remove(item);
                    _db.SaveChanges();

                    BindData();
                }
            }
                
        }
    }   
}
