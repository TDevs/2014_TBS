var AgentListController = function ($http, $scope, $location, MemberService) {

    //get list contact
    MemberService.AgentList($http, $scope);

    //start config paging
    $scope.aoColumns = [
        { "sTitle": "First Name" },
        { "sTitle": "Last Name" },
        { "sTitle": "Status" },
        { "sTitle": "Activated Date" },
          { "sTitle": "Chauffer Lic" },
        { "sTitle": "Mobile Phone" },
        { "sTitle": "Home Phone" },
         { "sTitle": "Vehicle Number" },
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"\" onclick=\"angular.element(this).scope().EditAgent('" + obj.aData.Agent.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"\" onclick=\"angular.element(this).scope().DelAgent('" + obj.aData.Agent.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Agent.FirstName", "aTargets": [0] },
                        { "mDataProp": "Agent.LastName", "aTargets": [1] },
                        { "mDataProp": "Agent.Status", "aTargets": [2] },
                        { "mDataProp": "Agent.ActiveDate", "aTargets": [3] },
                        { "mDataProp": "Agent.ChaufferLic", "aTargets": [4] },
                         { "mDataProp": "Agent.MobilePhone", "aTargets": [5] },
                        { "mDataProp": "Agent.HomePhone", "aTargets": [6] },
                        { "mDataProp": "VehicleName", "aTargets": [7] },
                        { "mDataProp": "Agent.Id", "aTargets": [8] }
    ];

    //end config paging 

    //click button add contact 
    $scope.AddAgent = function () {
        $location.path('/Member/NewAgent');
    }

    $scope.EditAgent = function(id) {
        var u = $.grep($scope.AgentList, function (e) { return e.Agent.Id == id; });
        var agent = u[0].Agent;
        MemberService.setAgent(agent);
        window.location.href = "#/Member/EditAgent";
        //$location.path("/Member/EditAgent");
    }

    $scope.DelAgent = function (id) {
        var u = $.grep($scope.AgentList, function (e) { return e.Agent.Id == id; });
        var agent = u[0].Agent;
        if (confirm("Are you sure you want to delete this agent?")) {
            $http({
                method: 'POST',
                url: '/Member/DelAgent',
                data: agent,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.AgentList = jQuery.grep($scope.AgentList, function (value) {
                        return value.Agent.Id != agent.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }

    }
}