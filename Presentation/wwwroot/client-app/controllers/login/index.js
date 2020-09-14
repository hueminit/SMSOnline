var loginController = function () {
    this.init = function () {
        registerEvents();
    }

    var registerEvents = function () {
        //$('#frmLogin').validate({
        //    errorClass: 'red',
        //    ignore: [], //
        //    lang: 'vi',
        //    rules: {
        //        userName: {
        //            required: true,
        //            minlength: 4,
        //            maxlength: 30
        //        },
        //        password: {
        //            required: true,
        //            minlength: 4,
        //            maxlength: 30
        //        }
        //    }

        //});

        $('#btnLogin').on('click', function (e) {
            // if ($('#frmLogin').valid()) {
            e.preventDefault();
            var user = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            login(user, password);
            // }
        });
    }

    var login = function (user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                //UserName: user,
                Email: user,
                Password: pass
            },
            dataType: 'json',
            url: 'Login/authen',
            success: function (res) {
                if (res.Success) {
                    window.location.href = "/Home/Index";
                    ;
                }
                else {
                    common.notify('Đăng nhập không thành công', 'error');
                }
            }
        })
    }
}