var Mapping = {
    'PurchaseDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemId);
        },
        create: function (options) {
            return new PurchaseItems(options.data);
        }
    }
};


PuchaseVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.AddNewItem = function () {
        var newItem = new PurchaseItems({
            StoreItemId: 0,
            CurrentQty: 0,
            QTY: 0,
            UnitPrice: 0,
            Total: 0,
            SN:''
        });

        self.PurchaseDetails.push(newItem);
    }

    self.RemoveItem = function (storeItem) {
        self.PurchaseDetails.remove(storeItem)
    }
    self.PurchaseSummary.TotalAmount = ko.pureComputed({
        read: function () {
            let total = 0;
            for (var i = 0; i < self.PurchaseDetails().length; i++) {
                total = total + parseFloat(self.PurchaseDetails()[i].QTY()) *
                    parseFloat(self.PurchaseDetails()[i].UnitPrice());
            }
            return total;
        },
        write: function (value) {
            return 0;
        },
        owner: self
    });

    self.ApplyVAT = function () {

        if (self.PurchaseSummary.IsVAT() === true) {
            var Taxs = parseFloat(self.PurchaseSummary.VATRate()) * parseFloat(self.PurchaseSummary.TotalAmount());
            self.PurchaseSummary.VATAmount(Taxs.toFixed(2));
            self.PurchaseSummary.TotalAmountWithVAT(parseFloat(self.PurchaseSummary.TotalAmount()) + parseFloat(self.PurchaseSummary.VATAmount()));
        }
        else {
            self.PurchaseSummary.VATAmount(0);
            self.PurchaseSummary.TotalAmountWithVAT(self.PurchaseSummary.TotalAmount());
        }

    }

    self.Save = function () {

        self.IsWaitingAreaVisible(true);
        self.IsDetailAreaVisible(false);
        var formData = new FormData();

        if (document.getElementById("InvoiceFile").files.length > 0) {
            var InvoiceFile = $('#InvoiceFile').get(0).files[0];
            formData.append('InvoiceFile', InvoiceFile);
        }

        var viewmodel = ko.toJSON(self);
        formData.append('vm', viewmodel);

        $.ajax({
            url: self.SaveURL(),
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.newLocation !== null && data.newLocation !== undefined) {
                    window.location = data.newLocation;
                }

                if (data.errors !== null && data.errors !== undefined) {
                    self.IsWaitingAreaVisible(false);
                    self.IsDetailAreaVisible(true);
                    self.IsMessageAreaVisible(true);
                    self.Messages(data.errors);
                }
            }
        });
    }
  
    
}


PurchaseItems = function (data) {

    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.Total = ko.computed(function () {
        return parseFloat(self.QTY()) * parseFloat(self.UnitPrice());
    });

    self.ChangeItem = function () {
        var itemId = self.StoreItemId();
        $.ajax({
            url: '/Expenditure/Supplier/GetItemBalance/'+itemId,
            type: "POST",
            data: ko.toJSON(itemId),
            contentType: "application/json",
            success: function (data) {
                if (data.CurrentQty !== null && data.CurrentQty !== undefined) {
                    self.CurrentQty(data.CurrentQty);

                }
                if (data.errors !== null && data.errors !== undefined) {

                    self.Messages(data.errors);
                }

            }
        });
    }
}