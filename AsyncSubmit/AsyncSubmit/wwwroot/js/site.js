$(document).ready(function () {
    $("#indexForm").submit(function (e) {
        if ($("#indexForm").valid()) {
            
            e.preventDefault();

            var actionUrl = $(this).attr("action");

            var requestContent = $(this).serialize();

            var submitedEmail = $("input[type=email]").val();

            $.ajax({
                method: "post",
                url: actionUrl,
                data: requestContent,
                success: function (data, textStatus, jqXHR) {
                    toastr.success("Submited successfully!", submitedEmail);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error("Something went wrong! Please try again in a short while.", jqXHR.status);
                }
            });
        }
    });
});