var ContactListController = function ($http, $scope, $location, MemberService)
{
    //get list contact
    MemberService.ContactList($http, $scope);

    //start config paging
    $scope.aoColumns = [
        { "sTitle": "First Name" },
        { "sTitle": "Last Name" },
        { "sTitle": "Title" },        
        { "sTitle": "Mobile Phone" },
        { "sTitle": "Home Phone" },
        
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"\" onclick=\"angular.element(this).scope().EditContact('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"\" onclick=\"angular.element(this).scope().DelContact('" + obj.aData.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "FirstName", "aTargets": [0] },
                        { "mDataProp": "LastName", "aTargets": [1] },
                        { "mDataProp": "Title", "aTargets": [2] },                       
                         { "mDataProp": "MobilePhone", "aTargets": [3] },
                        { "mDataProp": "HomePhone", "aTargets": [4] },                        
                        { "mDataProp": "Id", "aTargets": [5] }
    ];

    //end config paging 

    //click button add contact 
    $scope.AddContact = function ()
    {
        $location.path('/Member/NewContact');
    }

    $scope.EditContact = function (id) {
        var u = $.grep($scope.ContactList, function (e) { return e.Id == id; });
        var contact = u[0];
        MemberService.setContact(contact);
        window.location.href = "#/Member/EditContact";
       // $location.path("/Member/EditContact");
    }

    $scope.DelContact =  function (id) {
        var u = $.grep($scope.ContactList, function (e) { return e.Id == id; });
        var contact = u[0];
        if (confirm("Are you sure you want to delete this contact?")) {
            $http({
                method: 'POST',
                url: '/Member/DelContact',
                data: contact,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.ContactList = jQuery.grep($scope.ContactList, function (value) {
                        return value.Id != contact.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
        
    }
}