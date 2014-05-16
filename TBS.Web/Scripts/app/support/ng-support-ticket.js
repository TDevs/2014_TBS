var RaiseTicketController = function ($scope, $http, $location, $sce, SupportService) {

    $scope.IsSubmitted = false;
    $scope.TicketItems = new Array();
    $scope.ticket = {
        'Title': '',
        'CreateBy': '',
        'TicketItems': $scope.TicketItems
    };




    $scope.PostTicket = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {

            var newTicket = new Object();
            newTicket.Message = $scope.Message;
            $scope.TicketItems.push(newTicket);
            $http({
                method: 'POST',
                url: '/Support/PostRaiseTicket',
                data: $scope.ticket,
                async: false,
            }).success(function (data, status, headers, config) {
                alert('Your comment is posted!');
                document.location.href = '/Support/Ticket#/Support/ViewTicketList';
            });
        }
    };

    $scope.ViewTicket = function (ticket) {

        SupportService.setTicket(ticket);

        $location.path('/Support/ViewTicket');
    }

};

var TicketController = function ($scope, $http, $location, $sce, SupportService) {

    $scope.lstTickets = new Array();
    $scope.IsSubmitted = false;
    $scope.TicketItems = new Array();
    $scope.ticket = {
        'Title': '',
        'CreateBy': '',
        'Message': '',
        'TicketItems': $scope.TicketItems
    };



    //get tbs support
    loadTickets($http, $scope, $sce);

    var supportStatus = SupportService.supportStatus();


    $scope.ViewTicket = function (ticket) {

        SupportService.setTicket(ticket);

        $location.path('/Support/ViewTicket');
    }

};


var ViewTicketController = function ($scope, $http, $sce, $location, SupportService) {

    $scope.lstTickets = new Array();

    $scope.ticket = SupportService.getTicket();
    
    angular.forEach($scope.ticket.TicketItems, function (value, key) {

        value.Message = $sce.trustAsHtml(value.Message);
        $scope.lstTickets.push(value);
    })


    $scope.PostTicket = function (isValid) {


        $scope.IsSubmitted = true;

        if (!isValid) {

            $scope.ticket.TicketItems = new Array();
            var newTicket = new Object();
            newTicket.Message = $scope.Message;

            $scope.ticket.TicketItems.push(newTicket);

            $http({
                method: 'POST',
                url: '/Support/PostTicketMessage',
                data: $scope.ticket,
                async: false,
            }).success(function (data, status, headers, config) {
                alert('Your comment is posted!');
                document.location.href = '/Support/Ticket#/Support/ViewTicketList';
            });
        }
    };



};


function loadTickets($http, $scope, $sce) {
    $http({
        method: 'GET',
        url: '/Support/GetRaiseTicket',
        async: false,
    }).success(function (data, status, headers, config) {
        //$scope.Tickets = new Array();

        //angular.forEach(data, function (value, key) {
        //    $scope.lstTickets.push(angular.copy(value));
        //    value.Message = $sce.trustAsHtml(value.Message);
        //    $scope.Tickets.push(value);
        //})
        $scope.Tickets = data;
    })
}


//function loadTickets($http, $scope, $sce) {
//    $http({
//        method: 'GET',
//        url: '/Support/GetRaiseTicket',
//        async: false,
//    }).success(function (data, status, headers, config) {
//        $scope.Tickets = new Array();

//        angular.forEach(data, function (value, key) {
//            $scope.lstTickets.push(angular.copy(value));
//            value.Message = $sce.trustAsHtml(value.Message);
//            $scope.Tickets.push(value);
//        })

//    })
//}