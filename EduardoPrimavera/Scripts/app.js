//Home
var ViewModel = function () {
    var self = this;
    self.benefits = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

    var benefitsUri = '/api/benefits/';

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

    function getAllBenefits() {
        ajaxHelper(benefitsUri, 'GET').done(function (data) {
            self.benefits(data);
            console.log(self.benefits());
        });
    }

    self.getBenefitDetail = function (item) {
        self.detail(item);
    }

    self.redirect = function (item) {
        redURL();
    }

    function redURL() {
        window.open("/Edit?id=" + self.detail().Id, "_blank")
    }
    // Fetch the initial data.
    getAllBenefits();
};

ko.applyBindings(new ViewModel());

