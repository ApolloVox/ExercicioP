//Edit
var ViewModel = function () {
    var self = this;
    self.error = ko.observable();
    self.detail = ko.observable();
    self.cities = ko.observableArray();
    self.categories = ko.observableArray();
    

    self.nBenefit = {
        Name: ko.observable(),
        Description: ko.observable(),
        Category: ko.observable(),
        City: ko.observable()
    }

    var benefitsUri = '/api/benefits/';
    var categoriesUri = '/api/Categories/';
    var citiesUri = '/api/Cities/';

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

    function getBenefitDetail() {
        const urlParams = new URLSearchParams(window.location.search);
        const myParam = urlParams.get('id');
        ajaxHelper(benefitsUri + myParam, 'GET').done(function (data) {         
            self.detail(data);
            $("#inputname").val(self.detail().Name);
            $("#inputDescription").val(self.detail().Description);
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
        });
    }

    // Fetch the initial data.
    getBenefitDetail();
    getAllCities();
    getAllCategories();
};

ko.applyBindings(new ViewModel());

