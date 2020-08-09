var managementModule = (function () {

    // APIs
    return {
        refreshUsersList: refreshUsersList,
        getPermissionsOfUser: getPermissionsOfUser,
        clearUserSelection: clearUserSelection,
        addNewPermission: addNewPermission,
        bindNewPermissions: bindNewPermissions,
        unbindPermission: unbindPermission,
        addNewPermissionGroup: addNewPermissionGroup,
        bindNewPermissionGroup: bindNewPermissionGroup
    };


    // Public Methods
    function refreshUsersList() {
        usersTable.clear().draw();
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

    function bindNewPermissions() {

        var username = $("#txtUsername").val();
        var permissions = [];

        $("#disabledPermissionTable").find('input[type=checkbox]').each(function () {
            if ($(this).is(":checked")) {
                permissions.push($(this).attr("name"));
            }
        });

        var data = {
            username: username,
            permissions: permissions
        }

        $.ajax({
            url: "api/Management/BindNewPermissions",
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#addPermissionModal').modal('hide');
                var username = $("#txtUsername").val();
                var fullname = $("#txtFullname").val();
                getPermissionsOfUser(fullname, username);
            }
        });
    }

    function bindNewPermissionGroup() {

        var username = $("#txtUsername").val();
        var permissions = [];

        $("#permissionGroupTable").find('input[type=checkbox]').each(function () {
            if ($(this).is(":checked")) {
                permissions.push($(this).attr("name"));
            }
        });

        var data = {
            username: username,
            permissions: permissions
        }

        $.ajax({
            url: "api/Management/BindNewPermissionGroup",
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#addPermissionGroupModal').modal('hide');
                var username = $("#txtUsername").val();
                var fullname = $("#txtFullname").val();
                getPermissionsOfUser(fullname, username);
            }
        });
    }




    function unbindPermission(permission) {

        var username = $("#txtUsername").val();

        var data = {
            username: username,
            permission: permission
        }

        $.ajax({
            url: "api/Management/UnbindPermission",
            type: "GET",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var username = $("#txtUsername").val();
                var fullname = $("#txtFullname").val();
                getPermissionsOfUser(fullname, username);
            }
        });
    }




    function clearUserSelection() {
        $("#txtUsername").val("");
        $("#txtFullname").val("");
        $("#infobar").hide();
        $("#infobartext").html("No User Selected");
        $("#btnAddPermission").prop('disabled', true);
        $("#btnAddPermissionGroup").prop('disabled', true);
        $("#permissionviewTableContainer").hide();
    }

    function addNewPermission() {
        var username = $("#txtUsername").val();
        var param = {
            username: username
        }
        $.get("api/Management/GetDisabledPermissions", param, function (data) {
            disabledPermissionTable.clear().draw();
            for (var i = 0; i < data.length; i++) {
                disabledPermissionTable.row.add([
                    "",
                    data[i].permission,
                    data[i].description,
                ]).draw(false);
                $("#permissioninfo").html("Binding Permissions To - <b>" + $("#txtFullname").val().toUpperCase() + "</b>");
            }
            $('#addPermissionModal').modal('show');
        });
    }


    function addNewPermissionGroup() {
        var username = $("#txtUsername").val();
        var param = {
            username: username
        }
        $.get("api/Management/GetPermissionGroups", param, function (data) {            
            permissionGroupTable.clear().draw();
            for (var i = 0; i < data.length; i++) {
                permissionGroupTable.row.add([
                    "",
                    data[i].name,
                    data[i].description,
                ]).draw(false);
                $("#permissioninfo").html("Binding Permission Group To - <b>" + $("#txtFullname").val().toUpperCase() + "</b>");
            }
            $('#addPermissionGroupModal').modal('show');
        });
    }



    function showLoader(text) {
        $("#permissionviewTableContainer").hide();
        $("#userPermissionLoader").show();
        $("#userPermissionLoaderText").text(text);
    }

    function hideLoader() {
        setTimeout(function () {
            $("#permissionviewTableContainer").show();
            $("#userPermissionLoader").hide();
            $("#userPermissionLoaderText").text("Loading...");
        }, 300);
    }

    function getPermissionsOfUser(fullname, username) {
        $("#txtUsername").val(username);
        $("#txtFullname").val(fullname);
        permissionviewTable.clear().draw();
        $("#infobar").show();

        showLoader("Loading permissions...");


        $("#infobartext").html("Editing Permissions Bindings For - " + fullname.toUpperCase());
        var param = {
            username: username
        }
        $.get("api/Management/GetUserPermissions", param, function (data) {
            for (var i = 0; i < data.length; i++) {
                permissionviewTable.row.add([
                    data[i].permission,
                    data[i].description,
                    null
                ]).draw(false);
            }
            $("#btnAddPermission").prop('disabled', false);
            $("#btnAddPermissionGroup").prop('disabled', false);
            hideLoader();
        });
    }


})();