/*
     Author Name :     Tsaleni Dobson Maswanganye
     Date        :     2018 - 07 - 06
     Description :     Validate madatory fields
*/

var SelectField = function (selectCtrl) {
    this.selectCtrl = selectCtrl;
    this.attach("change");
};

SelectField.prototype = {
    attach: function (e) {
        var obj = this;
        if (e === "change") {
            obj.selectCtrl.bind(e, function () {
                obj.selectCtrl.parent().find("span").remove();
            });
        }
    },
    validate: function () {
        var obj = this;
        if (obj.selectCtrl.find("option:selected").val() === "0") {
            obj.selectCtrl.parent().find("span").remove();
            obj.selectCtrl.parent().find("label").each(function () {
                $("<span style='color: red;'> * required</span>").appendTo($(this));
            });
            return false;
        }
        else {
            return true;
        }
    }
};

var RequiredField = function (requiredCtrl) {
    this.requiredCtrl = requiredCtrl;
    this.attach("keyup");
};

RequiredField.prototype = {
    validate: function () {
        var obj = this;
        if (obj.requiredCtrl.val() === "") {
            obj.requiredCtrl.parent().find("span").remove();
            obj.requiredCtrl.parent().find("label").each(function (index) {
                $("<span style='color: red;'> * Required</span>").appendTo($(this));
            });
            return false;
        }
        else {
            return true;
        }
    },
    attach: function (e) {
        var obj = this;
        if (e === "keyup") {
            return obj.requiredCtrl.bind(e, function () {
                obj.requiredCtrl.parent().find("span").remove();
            });
        }
    }
};

var RequiredNumberField = function (requiredNumberCtrl) {
    this.requiredNumberCtrl = requiredNumberCtrl;
    this.attach("keyup");
};

RequiredNumberField.prototype = {
    validate: function () {
        var obj = this;
        if (obj.requiredNumberCtrl.val() === "0") {
            obj.requiredNumberCtrl.parent().find("span").remove();
            obj.requiredNumberCtrl.parent().find("label").each(function (index) {
                $("<span style='color: red;'> * Required</span>").appendTo($(this));
            });
            return false;
        }
        else {
            return true;
        }
    },
    attach: function (e) {
        var obj = this;
        if (e === "keyup") {
            return obj.requiredNumberCtrl.bind(e, function () {
                obj.requiredNumberCtrl.parent().find("span").remove();
            });
        }
    }
};

var RequiredDateField = function (requiredDateCtrl) {
    this.requiredDateCtrl = requiredDateCtrl;
    this.attach("keyup");
};

RequiredDateField.prototype = {
    validate: function () {
        var obj = this;
        if (obj.requiredDateCtrl.val() === "") {
            obj.requiredDateCtrl.parent().find("span").remove();
            obj.requiredDateCtrl.parent().find("label").each(function (index) {
                $("<span style='color: red;'> * Required</span>").appendTo($(this));
            });
            return false;
        }
        else {
            return true;
        }
    },
    attach: function (e) {
        var obj = this;
        if (e === "keyup") {
            return obj.requiredDateCtrl.bind(e, function () {
                obj.requiredDateCtrl.parent().find("span").remove();
            });
        }
    }
};

var RequiredTimeField = function (requiredTimeCtrl) {
    this.requiredTimeCtrl = requiredTimeCtrl;
    this.attach("keyup");
};

RequiredTimeField.prototype = {
    validate: function () {
        var obj = this;
        if (obj.requiredTimeCtrl.val() === "") {
            obj.requiredTimeCtrl.parent().find("span").remove();
            obj.requiredTimeCtrl.parent().find("label").each(function (index) {
                $("<span style='color: red;'> * Required</span>").appendTo($(this));
            });
            return false;
        }
        else {
            return true;
        }
    },
    attach: function (e) {
        var obj = this;
        if (e === "keyup") {
            return obj.requiredTimeCtrl.bind(e, function () {
                obj.requiredTimeCtrl.parent().find("span").remove();
            });
        }
    }
};

var Form = function (formCtrl) {
    this.formCtrl = formCtrl;
    var required_list = new Array();
    var select_list = new Array();
    var required_number_list = new Array();
    var required_date_list = new Array();
    var required_time_list = new Array();
    this.formCtrl.find("[validation=required]").each(function (index) {
        required_list[index] = new RequiredField($(this));
    });
    this.formCtrl.find("[validation=select]").each(function (index) {
        select_list[index] = new SelectField($(this));
    });
    this.formCtrl.find("[validation=number]").each(function (index) {
        required_number_list[index] = new RequiredNumberField($(this));
    });
    this.formCtrl.find("[validation=date]").each(function (index) {
        required_date_list[index] = new RequiredDateField($(this));
    });
    this.formCtrl.find("[validation=time]").each(function (index) {
        required_time_list[index] = new RequiredTimeField($(this));
    });
    this.requireList = required_list;
    this.selectList = select_list;
    this.required_number_list = required_number_list;
    this.required_date_list = required_date_list;
    this.required_time_list = required_time_list;
};

Form.prototype = {
    valid: function () {
        var obj = this,
            is_valid = true;
        for (var index = 0; index < obj.requireList.length; index++) {
            if (!obj.requireList[index].validate())
                is_valid = false;
        }

        for (var pIndex = 0; pIndex < obj.selectList.length; pIndex++) {
            if (!obj.selectList[pIndex].validate()) {
                is_valid = false;
            }
        }

        for (var nIndex = 0; nIndex < obj.required_number_list.length; nIndex++) {
            if (!obj.required_number_list[nIndex].validate())
                is_valid = false;
        }

        for (var dIndex = 0; dIndex < obj.required_date_list.length; dIndex++) {
            if (!obj.required_date_list[dIndex].validate())
                is_valid = false;
        }

        for (var tIndex = 0; tIndex < obj.required_time_list.length; tIndex++) {
            if (!obj.required_time_list[tIndex].validate())
                is_valid = false;
        }

        return is_valid;
    }
};
