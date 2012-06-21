/// <reference path="jquery-1.7.1.js" />
/*globals $, window */

$(function () {
    $.ajaxSetup({ cache: false });
});

(function (window) {
    var ajaxUtil = window.ajaxUtil = {};

    ajaxUtil.modify = function (url, dataToSave, httpVerb, successMessage, callback) {
        $.ajax(url, {
            data: dataToSave,
            type: httpVerb,
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                $.notifyBar({
                    html: successMessage,
                    cls: "success"
                });
                if (callback !== undefined) {
                    callback(data);
                }
            },
            error: function () {
                $.notifyBar({
                    html: "Unexpected error.",
                    cls: "error"
                });
            }
        });
    };

    ajaxUtil.add = function (url, dataToSave, callback) {
        this.modify(url, dataToSave, "POST", "Item Added.", callback);
    };

    ajaxUtil.update = function (url, dataToSave, successCallback) {
        this.modify(url, dataToSave, "PUT", "Item Updated.", successCallback);
    };

    ajaxUtil.remove = function (url) {
        this.modify(url, null, "DELETE", "Item Deleted.");
    };
}(window));