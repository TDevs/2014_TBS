var FAQListController = function ($scope, $http, $sce, $modal, RoleService) {

    $scope.lstFAQs = new Array();
    // Load FAQs
    LoadFAQs($http, $scope, $sce);

    $scope.AddFAQ = function () {

        var faq = {
            'Question': '',
            'Answer': '',
            'Predefined': '',

        };
        var modalInstance = $modal.open({
            templateUrl: '/Support/CreateFAQ',
            controller: 'FAQCreateController',
            resolve: {
                faq: function () {
                    return faq;
                }
            }
        });

        //reload list
        modalInstance.result.then(function () {
            // Load FAQs
            LoadFAQs($http, $scope, $sce);
        });
    };

    RoleService.GetCurrentRole($http, $scope);

    $scope.EditFAQ = function (eFaq) {
        var faq = null;
        var count = 0;

        // Get orginal before decode to HTML raw
        angular.forEach($scope.lstFAQs, function (value, key) {

            if (count == 0) {
                if (value.Id == eFaq.Id) {
                    faq = value;
                    count++;
                }
            }
        })

        var modalInstance = $modal.open({
            templateUrl: '/Support/EditFAQ',
            controller: 'FAQEditController',
            resolve: {
                faq: function () {
                    return angular.copy(faq);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            LoadFAQs($http, $scope, $sce)

        });
    };

    $scope.DeleteFAQ = function (faq) {
        if (confirm("Are you sure you want to delete this FAQ?")) {
            $http({
                method: 'POST',
                url: '/Support/DeleteFAQ',
                data: faq,
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.FAQs = jQuery.grep($scope.FAQs, function (value) {
                    return value.Id != faq.Id;
                });
            })
        }
    };


};


var FAQCreateController = function ($scope, $http, $modalInstance, faq) {

    $scope.FormTitle = "Create a new FAQ";
    $scope.faq = faq;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save user
            $http({
                method: 'POST',
                url: '/Support/CreateFAQ',
                data: $scope.faq,
                async: false
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
            });

        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

var FAQEditController = function ($scope, $http, $modalInstance, faq) {

    $scope.FormTitle = "Edit FAQ";

    var count = 0;


    $scope.faq = faq;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save EditRole
            $http({
                method: 'POST',
                url: '/Support/EditFAQ',
                data: $scope.faq,
                async: true
            });
            $modalInstance.close();
        }


    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};


function LoadFAQs($http, $scope, $sce) {
    $scope.FAQs = new Array();
    //save user
    $http({
        method: 'GET',
        url: '/Support/GetFAQs',
        async: false
    }).success(function (data, status, headers, config) {

        angular.forEach(data, function (value, key) {
            $scope.lstFAQs.push(angular.copy(value));
            value.Answer = $sce.trustAsHtml(value.Answer);
            $scope.FAQs.push(value);
        })
    });

}

var FAQsController = function ($scope, $http, $sce) {
    $scope.lstFAQs = new Array();
    LoadFAQs($http, $scope, $sce);

};

