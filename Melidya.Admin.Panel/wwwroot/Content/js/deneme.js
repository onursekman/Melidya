<script>
    $(document).ready(function () {

        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        })

            $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
        checkboxClass: 'icheckbox_minimal-red',
    radioClass: 'iradio_minimal-red'
})

            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
    radioClass: 'iradio_flat-green'
})
})

        $("#update-password").click(function () {
            var Password = document.getElementById("Password").value;
    var PasswordUpdate = document.getElementById("PasswordUpdate").value;

            var data = {
        UserId: $("#userId").val(),
    Password: Password,
    PasswordUpdate: PasswordUpdate,
}

            if (checkPassword(Password, PasswordUpdate)) {

        updatePassword(data);

    }
            else {

        updatePassword(data);
    }

})

        function checkPassword(password, passwordUpdate) {
            return password == passwordUpdate
    console.log(data);
}

        function updatePassword(data) {
        $.ajax({
            url: "/User/PasswordUpdate",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),

            success: function (result) {
                if (result.resultCode == 200) {
                    $("#divresult").removeClass("alert-danger");
                    $("#divresult").addClass("alert-success");
                }
                else if (result.resultCode == 304) {
                    $("#divresult").removeClass("alert-success");
                    $("#divresult").addClass("alert-danger");

                }
                else {
                    $("#divresult").removeClass("alert-success");
                    $("#divresult").removeClass("lblSucMessage");
                }
                $("#lblResultMessage").text(result.resultMessage);

                $("#divresult").show();

                setTimeout(function () { $("#divresult").hide('slow'); }, 1500);


            }

        })
    }

</script>