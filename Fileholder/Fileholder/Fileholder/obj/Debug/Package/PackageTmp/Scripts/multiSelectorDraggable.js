function attachConnectedSortable(element) {
    $(element).find(".connectedSortable").sortable({
        connectWith: ".connectedSortable",
        placeholder: "ui-state-highlight",
        update: readjustListSortable
    }).disableSelection();

}


//Adjust input that need to correspond to the li element that
//may have been moved. The value need to be changed to reflect
//their new index depending of which list they are now. This is called only when we transfert all from one list to another one and one item is moved.
//The whole list is recreated
//See GuidMultiSelector.cshtml
function adjustConnectedSortableInputValues(ulInput) {
    var userControl = $(ulInput).closest('.gridMultiSelector-container-choice');
    var selectorSelectedName = $(userControl).find('ul.guid-multi-selector-selected').attr('data-property-name');

    $(userControl).find("ul.guid-multi-selector-selected li").each(function (idx, li) {
        adjustConnectedSortableHiddenField(li, selectorSelectedName, idx);
    });
    selectorSelectedName = $(userControl).find('ul.guid-multi-selector-available').attr('data-property-name');
    $(userControl).find("ul.guid-multi-selector-available li").each(function (idx, li) {
        adjustConnectedSortableHiddenField(li, selectorSelectedName, idx);
    });
}

//Adjust a single hidden field value to reflect the list li
function adjustConnectedSortableHiddenField(liInputObj, selectorSelectedName, index) {
    /*var idInputs = $(liInputObj).children("input:hidden");
    $(idInputs).attr("id", selectorSelectedName + "_" + index + "__ID");
    $(idInputs).attr("name", selectorSelectedName + "[" + index + "].ID");*/
    $(liInputObj).children("input").each(function () {
        var propertyName = $(this).attr("name").substr($(this).attr("name").lastIndexOf(".") + 1);
        $(this).attr("id", selectorSelectedName + "_" + index + "__" + propertyName);
        $(this).attr("name", selectorSelectedName + "[" + index + "]." + propertyName);
    });
}


//Adjust the "read-only" field
function readjustListSortable(event, ui) {
    if ($(ui.sender).length != 0) {
        var ulParent = ui.item.parent(); //ul of the dropped (destination)
        readjustListSortableUl(ui.item, ulParent);
        adjustConnectedSortableInputValues(ulParent);
    }

}

function readjustListSortableUl(uiItemChanged, ulParent) {
    var selectedUl = $(ulParent).closest($(".gridMultiSelector-container-choice")).find('.guid-multi-selector-selected');
    var allSelected = "";
    selectedUl.find('li').each(function (idx, li) {
        allSelected += $(li).find('span.description').html() + ", ";
    });
    var control = $(ulParent).closest($('.gridMultiSelector-choice'));
    var divisionForSummaryText = $(control).parent().parent().find('.gridMultiSelector-description');
    if (allSelected == "") {
        allSelected = divisionForSummaryText.attr('data-noselection-text');
    }
    divisionForSummaryText.html(allSelected.replace(/(, $)/g, ''));

    //Call extended Javascript function
    callExtendedSortableFunctionOnItemChanged(uiItemChanged);
}

//Call the function with the item changed in the list. This function is set inside GuidMultiSelector.cshtml
//and is called only if specified.
//The parameter uiItemChanged is the item changed
function callExtendedSortableFunctionOnItemChanged(uiItemChanged) {
    var ulParent = $(uiItemChanged).parent();
    var functionName = $(ulParent).closest('.gridMultiSelector-choice').attr("data-onitemcreate-functionname");
    if (typeof (functionName) != "undefined" && functionName != "") {
        window[functionName]($(uiItemChanged));
    }
}

//Animation when clicking the textbox
function gridMultiSelectorOnclick(obj) {
    //If animated than we wait the animation to be over
    if ($(':animated').length) {
        return false;
    }

    var widthToGo = '612px';
    var marginLeftToGo = '0px';
    var heightToGo = '340px';
    var animation1First = true;
    if (obj.css('display') != 'none') {
        widthToGo = '310px';
        marginLeftToGo = '300px';
        animation1First = false;
        heightToGo = '0px';
    }

    if (animation1First) {
        obj.show().animate({
            height: heightToGo,
            easing: "easein"
        }, 400)
            .animate({
                easing: "easein",
                width: widthToGo,
                marginLeft: marginLeftToGo
            }, 500);
    } else {
        obj
                .animate({
                    easing: "easein",
                    width: widthToGo,
                    marginLeft: marginLeftToGo
                }, 400).animate({
                    height: heightToGo,
                    easing: "easein"
                }, 500, function () {
                    obj.hide();
                });
    }
}

/*Transfert all elements from the listFrom to the listTo*/
function transfertMultiSelectorList(listFrom, listTo) {
    $(listFrom).find('ul>li').each(
                    function (index, element) {
                        $(listTo).find('ul').append(element);
                    }
    );
    adjustConnectedSortableInputValues(listTo);
    readjustListSortableUl(listTo.find('ul'), listFrom);
}

$(document).ready(function () {
    attachConnectedSortable(this);
	
	/*Transfert every item from one list to the other*/
    $('.allavailableButton, .allselectedButton').click(function() {
        transfertMultiSelectorList($(this).closest('.gridMultiSelector-choice-single'),
            $($(this).closest('.gridMultiSelector-choice-single').siblings()[0])
        );
        return false;
    });

    $('.gridMultiSelector-choice').find('li').each(function() {
        callExtendedSortableFunctionOnItemChanged($(this));
    });
});