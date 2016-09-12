<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProteinTracker.aspx.cs" Inherits="ProteinTracker_WebService.ProteinTracker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Protein Tracker</title>
    <script type="text/javascript" src="Scripts/jquery-2.2.3.js"></script>
    <script>
        var _users;
        $(document).ready(function () {

            $('#select-user').change(function () {
                updateUserData();
            });

            populateSelectUsers();
        });


        function updateUserData() {
            var index = $('#select-user')[0].selectedIndex;
            if (index < 0)
                return;

            $('#outputTotal').text(_users[index].Total);
            $('#user-goal').text(_users[index].Goal);


        }

        function addNewUser() {
            var name = $('#name').val();
            var goal = $('#goal').val();

            //// using JQuery Ajax to call service method
            $.ajax({
                type: "POST",
                url: "ProteinTracker.aspx/AddUser",
                data: JSON.stringify({ "name": name, "goal": goal }),
                contentType: "application/json; charset=utf-8",
                dataType:"json",
                success: function () {
                    populateSelectUsers();
                },
                error: function () {
                    alert("something went wrong!!");
                }
            });
            //PageMethods.AddUser(name, goal, function () {
            // ProteinTracker_WebService.ProteinTrackingService.AddUser(name, goal, function () {

            //     populateSelectUsers();
            // });
        }

        function addProtein() {

            var amount = $('#amount').val();
            var userId = parseInt($('#select-user option:selected').val());

            PageMethods.AddProtein(parseInt(amount), userId, function (total) {

                //ProteinTracker_WebService.ProteinTrackingService.AddProtein(parseInt(amount), userId, function (total) {

                $('#outputTotal').html(total);
            });
        }

        function populateSelectUsers() {
            var selectUser = $('#select-user');
            selectUser.empty();

            PageMethods.ListUsers(function (users) {
                //ProteinTracker_WebService.ProteinTrackingService.ListUsers(function (users) {

                _users = users;
                for (var i = 0; i < users.length; i++) {
                    selectUser.append($('<option></option>').attr("value", users[i].UserId).text(users[i].UserName));
                }

                updateUserData();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="True">
            <Services>
                <asp:ServiceReference Path="ProteinTrackingService.asmx" />
            </Services>
        </asp:ScriptManager>
        <h1>Protein Tracker</h1>

        <div>
            <label for="select-user">select user:</label>
            <select id="select-user">
            </select>
        </div>
        <hr />
        <div>
            <h2>Add new User</h2>
            <br />
            <label for="name">Name</label>
            <input id="name" type="text" value="" /><br />
            <label for="goal">Goal</label>
            <input id="goal" type="number" min="0" max="100" /><br />
            <input id="btn-add-user" type="button" onclick="addNewUser()" value="Add" />
        </div>
        <hr />
        <div>
            <h2>Add Protein</h2>
            <br />
            <label for="amount">Protein</label>
            <input id="amount" type="number" min="0" /><br />

            <input id="btn-add-protein" type="button" onclick="addProtein()" value="Add" />
        </div>
        <div>
            <label for="outputTotal">Total</label>
            <label id="outputTotal"></label>
            <br />
            <label for="user-goal">Goal</label>
            <label id="user-goal"></label>
            <br />
        </div>
    </form>
</body>
</html>
