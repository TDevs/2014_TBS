var CreateContactController = function ($http, $scope, $location , MemberService)
{    
    $scope.States = MemberService.States().then(function (result) { $scope.States = result; });;
    
    $scope.IsSubmitted = false;
    $scope.Contact = {
        'FirstName': '',
        'LastName': '',
        'Address': '',
        'Address1': '',
        'City': '',
        'State': '',
        'Zip': '',
        'AgentAssigned': false,
        'Title': '',
        'DateOfBirth':  '',
        'Comments': '',
        'MobilePhone': '',
        'HomePhone': '',
        'SSN': '',
        'ChaufferLic': '',
        'DriveLic': '',
        'ContinueEducation': '',
        'IsStockholder':false,
        'UseThisAddresForMemberBilling': false,
        'UseThisContactAsRegisteredAgent': false,        
        'PictureContact': '',
        'MemberId': ''       
    };
    UploadImage("divUploadImage", "/Member/UploadFileImage");

    $scope.Member = MemberService.getMember();
    //save member 
    $scope.Save = function (inValid) {
        $scope.IsSubmitted = true;
        if (!inValid && $scope.Member != undefined) {
            $scope.Contact.MemberId = $scope.Member.Id
            //save bank
            $http({
                method: 'POST',
                url: '/Member/InsertContact',
                data: $scope.Contact,
                async: false
            }).success(function (data, status, headers, config) {
                alert('Save Contact Info success!');
                $("#search").attr("class", "");
                $("#info").attr("class", "active");
                $("#cash").attr("class", "");
                $location.path('/Member/ListContact');
            });
        }
    };


    $scope.Cancel = function () {

        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path("/Member/ListContact");
    }

    function UploadImage (id, urlServer) {
        // Initialize the jQuery File Upload plugin
        $('#' + id).fileupload({
            url: urlServer,
            type: 'post',
            dataType: 'json',
            autoUpload: true,
            maxFileSize: 10000000,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            formData: {},
            // This function is called when a file is added to the queue;
            // either via the browse button, or via drag/drop:
            add: function (e, data) {
                // Automatically upload the file once it is added to the queue
                var jqXHR = data.submit();

            },
            success: function (result, textStatus, jqXHR) {
                $scope.Contact.PictureContact = result;
                $scope.$apply();
            },
            fail: function (e, data) {
                // Something has gone wrong!
                data.context.addClass('error');
            }

        });
    }

}