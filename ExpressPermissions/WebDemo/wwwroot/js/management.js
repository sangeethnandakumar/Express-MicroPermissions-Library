var managementModule = (function () {

    // APIs
    return {
        refreshUsersList: refreshUsersList,
        getPermissionsOfUser: getPermissionsOfUser,
        clearUserSelection: clearUserSelection
    };


    // Public Methods
    function refreshUsersList() {        
        $.get("api/Management/GetUsers", function (data) {
            for (var i = 0; i < data.length; i++) {
                usersTable.row.add([
                    data[i].id,
                    data[i].username,
                    data[i].fullName,
                ]).draw(false);
            }            
        });
    }

    function clearUserSelection() {
        $("#infobar").hide();
        $("#infobar").html("No User Selected");
    }

    function getPermissionsOfUser(fullname, username) {
        $("#infobar").show();
        $("#infobar").html("Editing Permissions Binded To User - <b>" + fullname.toUpperCase() + "</b>");
        var param = {
            username: username
        }
        $.get("api/Management/GetUserPermissions", param, function (data) {
            debugger;
            for (var i = 0; i < data.length; i++) {
                permissionviewTable.row.add([
                    data[i].id,
                    data[i].username,
                    data[i].fullName,
                ]).draw(false);
            }            
        });
    }

    
})();