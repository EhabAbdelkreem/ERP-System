var Mapping = {
    'SalesItemDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemId);
        },
        create: function (options) {
            return new SalesItems(options.data);
        }
    }
};


SalesVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.AddNewItem = function () {
        var newItem = new SalesItems({
            StoreItemId: 0,
            CurrentQty: 0,
            QTY: 0,
            UnitPrice: 0,
            Total: 0,
        });

        self.SalesItemDetails.push(newItem);
    }

    self.RemoveItem = function (storeItem) {
        self.SalesItemDetails.remove(storeItem)
    }
    self.SalesSummary.TotalAmount = ko.pureComputed({
        read: function () {
            let total = 0;
            for (var i = 0; i < self.SalesItemDetails().length; i++) {
                total = total + parseFloat(self.SalesItemDetails()[i].QTY()) *
                    parseFloat(self.SalesItemDetails()[i].UnitPrice());
            }
            return total;
        },
        write: function (value) {
            return 0;
        },
        owner: self
    });

    self.ApplyVAT = function () {

        if (self.SalesSummary.IsVAT() === true) {
            var Taxs = parseFloat(self.SalesSummary.VATRate()) * parseFloat(self.SalesSummary.TotalAmount());
            self.SalesSummary.VATAmount(Taxs.toFixed(2));
            self.SalesSummary.TotalWithVAT(parseFloat(self.SalesSummary.TotalAmount()) +
                parseFloat(self.SalesSummary.VATAmount()));
        }
        else {
            self.SalesSummary.VATAmount(0);
            self.SalesSummary.TotalWithVAT(self.SalesSummary.TotalAmount());
        }

    }

    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: self.SaveURL(),
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.newLocation !== null && data.newLocation !== undefined) {
                    window.location = data.newLocation;

                }
                if (data.errors !== null && data.errors !== undefined) {

                    self.Messages(data.errors);
                }

            }
        });
    };


}


SalesItems = function (data) {

    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.Total = ko.computed(function () {
        return parseFloat(self.QTY()) * parseFloat(self.UnitPrice());
    });
    self.ChangeItem = function () {
        var itemId = self.StoreItemId();
        $.ajax({
            url: '/Expenditure/Supplier/GetItemBalance/' + itemId,
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