/// <reference path="knockout-2.1.0.js" />
/// <reference path="jquery-ui-1.8.18.js" />
/// <reference path="jquery-1.7.1.js" />
/// <reference path="ajax-util.js" />
/// <reference path="ko-protected-observable.js" />

$(function () {
    $("#tagDialog").hide();

    $.getJSON("/api/tags", function (data) {
        var viewModel = {
            // data
            tagsLink: data.SelfLink,
            tags: ko.observableArray(ko.toProtectedObservableItemArray(data.Items)),
            tagToAdd: ko.observable(""),
            selectedTag: ko.observable(new ko.protectedObservableItem(data.Items[0])),

            // behaviors
            addTag: function () {
                var newTag = { Name: this.tagToAdd() };
                this.tagToAdd("");

                ajaxUtil.add(this.tagsLink, ko.toJSON(newTag), function (data) {
                    viewModel.tags.push(new ko.protectedObservableItem(data));
                });
            },

            selectTag: function () {
                viewModel.selectedTag(this);
            },

            // Data (Drills)
            currentTagDrills: ko.observableArray([]),
            drillsLink: ko.observable(),
            drillToAdd: ko.observable(""),
            useDrillEditTemplate: ko.observable(null),
            hoverDrill: ko.observable(),
            clickedDrill: ko.observable(null),

            // Behaviors (Drills)
            editDrill: function () {
                viewModel.useDrillEditTemplate(true);
            },

            tagNameFor: function (id) {
                var tagItem = ko.utils.arrayFirst(viewModel.tags(), function (item) {
                    return item.Id() === parseInt(id);
                });
                return tagItem.Name;
            },

            saveDrill: function () {
                viewModel.selectedDrill().commit();
                var drill = viewModel.selectedDrill();
                viewModel.useDrillEditTemplate(null);
                ajaxUtil.update(drill.SelfLink(), ko.toJSON(drill));
            },

            cancelDrillEdit: function () {
                viewModel.useDrillEditTemplate(null);
            },

            addDrill: function () {
                var newDrill = { Name: this.drillToAdd(), TagId: this.selectedTag().Id };
                this.drillToAdd("");

                ajaxUtil.add(this.drillsLink(), ko.toJSON(newDrill), function (data) {
                    viewModel.currentTagDrills.push(new ko.protectedObservableItem(data));
                });
            },

            drillMouseOver: function () {
                viewModel.hoverDrill(this);
            },

            drillMouseOut: function () {
                viewModel.hoverDrill(null);
            },

            drillClick: function () {
                viewModel.clickedDrill(this);
            },

            isClicked: function () {
                return this === viewModel.clickedDrill();
            }
        }; // end viewModel

        $(".tag-delete").live("click", function () {
            var itemToRemove = ko.dataFor(this);
            viewModel.tags.remove(itemToRemove);
            ajaxUtil.remove(itemToRemove.SelfLink());
        });

        $(".tag-edit").live("click", function () {
            viewModel.selectedTag(ko.dataFor(this));
            $("#tagDialog").dialog({
                buttons: {
                    Save: function () {
                        $(this).dialog("close");
                        viewModel.selectedTag().Name.commit();
                        ajaxUtil.update(viewModel.selectedTag().SelfLink(), ko.toJSON(viewModel.selectedTag()));
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

        $(".drill-delete").live("click", function () {
            var itemToRemove = ko.dataFor(this);
            viewModel.currentTagDrills.remove(itemToRemove);
            ajaxUtil.remove(itemToRemove.SelfLink());
            viewModel.clickedDrill(null);
            viewModel.hoverDrill(null);
        });

        viewModel.selectedDrill = ko.computed(function () {
            var hoverDrill = this.hoverDrill();
            var clickedDrill = this.clickedDrill();
            return hoverDrill ? hoverDrill : (clickedDrill ? clickedDrill : this.currentTagDrills()[0]);
        }, viewModel);

        ko.computed(function () {
            $.getJSON(this.selectedTag().Links()[0].Href, function (data) {
                var drillLink = ko.utils.arrayFirst(data.Links, function (item) {
                    return item.LinkName == "add-drill";
                });
                viewModel.drillsLink(drillLink.Href);
                viewModel.currentTagDrills(ko.toProtectedObservableItemArray(data.Items));
            });
        }, viewModel);

        ko.applyBindings(viewModel);
    });
});
