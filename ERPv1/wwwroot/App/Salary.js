var Mapping = {
    'EmployeeSalaries': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.EmployeeId);
        },
        create: function (options) {
            return new SalaryDetail(options.data);
        }
    }
};

SalaryDetail = function (data) {

    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.GrossIncome = ko.pureComputed({
        read: function () {
            let total = parseFloat(self.BasicSalary()) + parseFloat(self.Allowonces())
                + parseFloat(self.Overtime()) + parseFloat(self.Commision())
                + parseFloat(self.InsuranceEmployer());
            
            return total;
        },
        write: function (value) {
            return 0;
        },
        owner: self
    });

    self.TotalDeductions = ko.pureComputed({
        read: function () {
            let total = parseFloat(self.InsuranceEmployee()) + parseFloat(self.Tax())
                + parseFloat(self.StaffAdvance()) + parseFloat(self.Penalties())
                + parseFloat(self.InsuranceEmployer());

            return total.toFixed(2);
        },
        write: function (value) {
            return 0;
        },
        owner: self
    });

    self.NetIncome = ko.pureComputed({
        read: function () {
            let total = parseFloat(self.GrossIncome()) - parseFloat(self.TotalDeductions());
            return total;
        },
        write: function (value) {
            return 0;
        },
        owner: self
    });
}

SalaryVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.CalcTax = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: "/Salary/CalcTax",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                self.EmployeeSalaries.removeAll();
                if (data.result !== null && data.result !== undefined) {
                    ko.mapping.fromJS(data.result, Mapping, self);
                }
                
            }
        });
    };

    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: '/Salary/SaveBatch',
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

    self.TotalBasicSalary = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].BasicSalary());
        }
        return Total
    });
    self.TotalAllownces = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].Allowonces());
        }
        return Total
    });
    self.TotalOvertime = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].Overtime());
        }
        return Total
    });
    self.TotalOvertime = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].Overtime());
        }
        return Total
    });
    self.TotalCommision = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].Commision());
        }
        return Total
    });
    self.TotalInsuranceEmployer = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].InsuranceEmployer());
        }
        return Total
    });
    self.TotalGrossIncome = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].GrossIncome());
        }
        return Total
    });

    self.TotalGrossIncome = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].GrossIncome());
        }
        return Total
    });

    self.TotalInsuranceEmployee = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].InsuranceEmployee());
        }
        return Total
    });

    self.TotalTax = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].Tax());
        }
        return Total
    });

    self.TotalStaffAdvance = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].StaffAdvance());
        }
        return Total
    });
    self.TotalPenalties = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].Penalties());
        }
        return Total
    });
    self.TotalTotalDeductions = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].TotalDeductions());
        }
        return Total
    });
    self.TotalNetIncome = ko.computed(function () {
        let Total = 0;
        for (var i = 0; i < self.EmployeeSalaries().length; i++) {
            Total = Total + parseFloat(self.EmployeeSalaries()[i].NetIncome());
        }
        return Total
    });
};
