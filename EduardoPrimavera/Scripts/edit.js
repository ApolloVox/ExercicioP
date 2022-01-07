//Edit
var ViewModel = function () {
    var self = this;
    self.error = ko.observable();
    self.sucess = ko.observable();
    self.detail = ko.observable();
    self.cities = ko.observableArray();
    self.categories = ko.observableArray();
    self.documents = ko.observableArray();
    self.document = ko.observable();


    self.nBenefit = {
        Name: ko.observable(),
        Description: ko.observable(),
        Category: ko.observable(),
        City: ko.observable()
    }

    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('id');
    var benefitsUri = '/api/benefits/';
    var categoriesUri = '/api/Categories/';
    var citiesUri = '/api/Cities/';
    var documentsUri = '/api/Documents/';
    var documentsIdUri = 'api/CustomeDoc/';

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

    function getBenefitDetail() {

        ajaxHelper(benefitsUri + myParam, 'GET').done(function (data) {
            self.detail(data);
            $("#inputname").val(self.detail().Name);
            $("#inputDescription").val(self.detail().Description);
        });
        ajaxHelper(documentsIdUri + myParam, 'GET').done(function (data) {
            self.documents(data);
        });
    }

    self.deleteDocument = function (item) {
        ajaxHelper(documentsUri + item.Id, 'DELETE').done(function (data) {
            console.log("Deleted");
            ajaxHelper(documentsIdUri + myParam, 'GET', myParam).done(function (data) {
                self.documents(data);
            });
        });
    }

    self.downloadDocument = function (item) {
        ajaxHelper(documentsUri + item.Id, 'GET').done(function (data) {
            self.document(data);
            console.log(self.document());
            download(self.document().File, self.document().Name, "application/pdf" );
        });
    }

    function getAllCities() {
        ajaxHelper(citiesUri, 'GET').done(function (data) {
            console.log(data);
            self.cities(data);
        });
    }

    function getAllCategories() {
        ajaxHelper(categoriesUri, 'GET').done(function (data) {
            console.log(data);
            self.categories(data);
        });
    }

    self.editBenefit = function (formElement) {
        var benefit = {
            Name: self.nBenefit.Name() ? self.nBenefit.Name() : self.detail().Name,
            Description: self.nBenefit.Description() ? self.nBenefit.Description() : self.detail().Description,
            CityId: self.nBenefit.City().Id,
            CategoryId: self.nBenefit.Category().Id,
            Id: self.detail().Id
        };
        console.log(benefit);

        ajaxHelper(benefitsUri + self.detail().Id, 'PUT', benefit).done(function (item) {
            self.sucess("Sucesso!");

        });
    }

    // Fetch the initial data.
    getBenefitDetail();
    getAllCities();
    getAllCategories();
};

ko.applyBindings(new ViewModel());

