$(document).ready(function () {
    $("#indexForm").submit(function (e) {
        if ($("#indexForm").valid()) {
            
            e.preventDefault();

            var actionUrl = $(this).attr("action");

            var requestContent = $(this).serialize();

            var submitedEmail = $("input[type=email]").val();

            //$.ajax({
            //    url: actionUrl,
            //    data: requestContent,
            //    success: function (data, textStatus, jqXHR) {
            //        toastr.success("submited successfully!", submitedEmail),
            //    },
            //    error: function (jqXHR, textStatus, errorThrown) {
            //        toastr.error(textStatus, jqXHR.status);
            //    }
            //});

            $.post(actionUrl, requestContent, function (xhr, res) {
                console.log(res.status);
                toastr.success("submited successfully!", submitedEmail);
            });
        }
    });
});