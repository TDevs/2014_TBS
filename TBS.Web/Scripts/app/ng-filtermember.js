var FilterMemberController = function ($http, $scope, $location, MemberService) {
    $scope.filterMember = function () {
        if ($scope.Search != '' && MemberService.getMember() != null) {
            $scope.CriterialSearch = MemberService.getCriterialSearch();
            //$scope.CriterialSearch.SearchBy = 8;
            //$scope.CriterialSearch.ViewDeleted = false;
            $scope.CriterialSearch.SearchFilter = $scope.Search;
            $http({
                method: 'POST',
                url: '/Member/SearchByCriterial',
                async: true,
                data: $scope.CriterialSearch
            }).success(function (data, status, headers, config) {
                //$scope.SearchFilterMember = data;
                //if (data != '')
                MemberService.setSearchFilter(data);
                //else
                //MemberService.setSearchFilter(MemberService.getMember());
            })
        }
        else {
            MemberService.setSearchFilter(MemberService.getMember());
        }
    };
}