//Home
var ViewModel = function () {
    var self = this;
    self.benefits = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.sucess = ko.observable();

    var benefitsUri = '/api/benefits/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        self.sucess('');
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
        $("#itemDetail").removeClass("invisible");
    }

    self.redirect = function () {
        redURL();
    }

    function redURL() {
        window.open("/Edit?id=" + self.detail().Id, "_blank")
    }

    self.del = function () {
        ajaxHelper(benefitsUri + self.detail().Id, 'DELETE', self.detail().Id ).done(function (data) {
            self.sucess("Sucesso!");
            getAllBenefits();
            $("#itemDetail").addClass("invisible");
        });
    }
    // Fetch the initial data.
    getAllBenefits();
};

ko.applyBindings(new ViewModel());

