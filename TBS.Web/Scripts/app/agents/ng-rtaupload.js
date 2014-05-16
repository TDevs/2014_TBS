var RTAUploadController = function ($http, $scope, $q, $location, MemberService) {

    UploadImage("divUploadImage", '/Member/RTAUploadFile')
    function UploadImage(id, urlServer) {
        // Initialize the jQuery File Upload plugin
        $('#' + id).fileupload({
            url: urlServer,
            type: 'get',
            dataType: 'json',
            autoUpload: true,
            maxFileSize: 10000000,
            acceptFileTypes: /(\.|\/)(xls?x)$/i,
            formData: {},
            // This function is called when a file is added to the queue;
            // either via the browse button, or via drag/drop:
            add: function (e, data) {
                // Automatically upload the file once it is added to the queue
                var jqXHR = data.submit();

            },
            success: function (result, textStatus, jqXHR) {
                if (result !=undefined && result!=null)
                {
                    GetList();
                }
            },
            fail: function (e, data) {
                // Something has gone wrong!
                alert("Occured error during upload file.Please try again!");
            }

        });
    }
    function GetList()
    {
        $http({
            method: 'POST',
            url: '/Member/GetRTAList',
            async: false,
        }).success(function (data, status, headers, config) {
            if (data != undefined && data != "") {
                $scope.RTAList = data;
            }

        })
    }
};