//Add
var ViewModel = function () {
    var self = this;
    self.error = ko.observable();
    self.sucess = ko.observable();
    self.detail = ko.observable();
    self.cities = ko.observableArray();
    self.categories = ko.observableArray();
    self.files = ko.observableArray();


    self.nBenefit = {
        Name: ko.observable(),
        Description: ko.observable(),
        Category: ko.observable(),
        City: ko.observable(),
        Documents: ko.observableArray()
    }

    var benefitsUri = '/api/benefits/';
    var categoriesUri = '/api/Categories/';
    var citiesUri = '/api/Cities/';
    var documentsUri = '/api/Documents/';

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

    function getAllCities() {
        ajaxHelper(citiesUri, 'GET').done(function (data) {
            self.cities(data);
        });
    }

    function getAllCategories() {
        ajaxHelper(categoriesUri, 'GET').done(function (data) {
            console.log(data);
            self.categories(data);
        });
    }

    self.addBenefit = function (formElement) {

        var benefit = {
            Name: self.nBenefit.Name() ? self.nBenefit.Name() : self.detail().Name,
            Description: self.nBenefit.Description() ? self.nBenefit.Description() : self.detail().Description,
            CityId: self.nBenefit.City().Id,
            CategoryId: self.nBenefit.Category().Id
        };

        ajaxHelper(benefitsUri, 'POST', benefit).done(function (item) {
            var files = $('#inputFile')[0].files;
            for (let i = 0; i < files.length; i++) {

                const toBase64 = file => new Promise((resolve, reject) => {
                    const reader = new FileReader();
                    reader.readAsDataURL(file);
                    reader.onload = () => resolve(reader.result);
                    reader.onerror = error => reject(error);
                });

                toBase64(files.item(i)).then(function (a) {
                    var document = {
                        Name: files.item(i).name,
                        File: a,
                        BenefitId: item.Id
                    };
                    ajaxHelper(documentsUri, 'POST', document).done(function (item2) {
                        if (i + 1 == files.length) {
                            self.sucess("Sucesso!");
                        }
                    });
                });
            }          
        });
    }

    // Fetch the initial data.
    getAllCities();
    getAllCategories();
};

ko.applyBindings(new ViewModel());

