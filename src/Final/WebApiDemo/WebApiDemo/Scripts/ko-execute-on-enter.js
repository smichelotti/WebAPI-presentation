/// <reference path="knockout.js" />
/// <reference path="jquery-1.7.1.js" />
/*globals $, ko */

ko.bindingHandlers.executeOnEnter = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        $(element).keypress(function (event) {
            var keyCode = event.which || event.keyCode;
            if (keyCode === 13) {
                value.call(viewModel);
                return false;
            }
            return true;
        });
    }
};