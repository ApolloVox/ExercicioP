//Edit
var ViewModel = function () {
    var self = this;
    self.error = ko.observable();
    self.loginUser = {
        User: ko.observable(),
        Password: ko.observable(),
    }

    var usersUri = '/api/Users/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    self.login = function (formElement) {

        var login = {
            Name: self.loginUser.User(),
            Password: self.loginUser.Password()
        };
        console.log(login);

        ajaxHelper(usersUri, 'POST', login).done(function (item) {
            debugger
            const d = new Date();
            d.setTime(d.getTime() + (1 * 24 * 60 * 60 * 1000));
            let expires = "expires=" + d.toUTCString();
            document.cookie = "session" + "=" + item + ";" + expires + ";path=/";
            if (item != null) {
                window.open("/Home", "_self");
            }
        });
    }

};

ko.applyBindings(new ViewModel());

