//RENDER CONTEXT MENU
onLoad = function (e) {  
    //PLANT
    $('#TreeSBS').data('tTreeView').addContextMenu({
        evaluateNode: function (treeview, node) {
            var nodeValue = treeview.getItemValue(node);
            return ((node.find('ul').length >= 0) && (nodeValue.substring(0, 5) == 'PLANT'));
        },
        menuItems: [
            {
                text: '+Area',
                onclick: MyTreeView_OnAdd
            }
        ]
    });

    //EQUIPMENT
    $('#TreeSBS').data('tTreeView').addContextMenu({
        evaluateNode: function (treeview, node) {
            var nodeValue = treeview.getItemValue(node);
            return ((node.find('ul').length >= 0) && (nodeValue.substring(0, 9) == 'EQUIPMENT'));
        },
        menuItems: [
            {
                text: 'Edit',
                onclick: MyTreeView_OnEdit
            },
            {
                text: 'Delete',
                onclick: MyTreeView_OnDelete
            },
        ]
    });

    //AREA
    $('#TreeSBS').data('tTreeView').addContextMenu({
        evaluateNode: function (treeview, node) {
            var nodeValue = treeview.getItemValue(node);
            return ((node.find('ul').length >= 0) && (nodeValue.substring(0, 4) == 'AREA'));
        },
        menuItems: CreateContextMenu('Unit')
    });

    //UNIT
    $('#TreeSBS').data('tTreeView').addContextMenu({
        evaluateNode: function (treeview, node) {
            var nodeValue = treeview.getItemValue(node);
            return ((node.find('ul').length >= 0) && (nodeValue.substring(0, 4) == 'UNIT'));
        },
        menuItems: CreateContextMenu('System')
    });

    //SYSTEM
    $('#TreeSBS').data('tTreeView').addContextMenu({       
        evaluateNode: function (treeview, node) {
            var nodeValue = treeview.getItemValue(node);
            return ((node.find('ul').length >= 0) && (nodeValue.substring(0, 6) == 'SYSTEM'));
        },
        menuItems: CreateContextMenu('Equipment-Group')
    });

    //EQUIPMENT GROUP
    $('#TreeSBS').data('tTreeView').addContextMenu({
        evaluateNode: function (treeview, node) {
            var nodeValue = treeview.getItemValue(node);
            return ((node.find('ul').length >= 0) && (nodeValue.substring(0, 15) == 'EQUIPMENT_GROUP'));
        },
        menuItems: CreateContextMenu('Equipment')
    });
}

MyTreeView_OnAdd = function (e) {
    var value = e.treeview.getItemValue(e.node);
    DeselectNode();
    SelectNode(value);
    value = value.split(';');
    $('#content').load(ReturnLink(value, 'Create'));
    //alert('You Clicked ' + e.item.text() + ' for ' + e.treeview.getItemText(e.node) + ' with a value of ' + e.treeview.getItemValue(e.node));
}

MyTreeView_OnEdit = function (e) {
    var value = e.treeview.getItemValue(e.node);
    DeselectNode();
    SelectNode(value);
    value = value.split(';');
    $('#content').load(ReturnLink(value,'Edit')+value[1]);
    //alert('You Clicked ' + e.item.text() + ' for ' + e.treeview.getItemText(e.node) + ' with a value of ' + e.treeview.getItemValue(e.node));
}

MyTreeView_OnDelete = function (e) {
    //alert('You Clicked ' + e.item.text() + ' for ' + e.treeview.getItemText(e.node) + ' with a value of ' + e.treeview.getItemValue(e.node));
    var value = e.treeview.getItemValue(e.node);
    DeselectNode();
    SelectNode(value);
    value = value.split(';');
    var values =
    {
        "Id": value[1]
    }
    var answer = confirm("Hapus " + value[0] + " " + e.treeview.getItemText(e.node) + " ?")
    if (answer) {
        $.post(ReturnLink(value, 'Delete'), values, function (data) {
            if (data) {
                RemoveItem(e);
                $('#content').html('<div id="hapus">'+value[0] + ' ' + e.treeview.getItemText(e.node)+' berhasil dihapus</div>');
            } else {
                alert("Data " + value[0] + " gagal dihapus");
            }
        });
    }

}

//REDIRECT LINK WHEN CONTEXT MENU SELECTED
function ReturnLink(value, action) {
    if (action == 'Create') {
        if (value[0] == 'PLANT') {
            return 'Foc/' + action + '/'+value[1];
        } else if (value[0] == 'AREA') {
            return 'Unit/' + action + '/' + value[1];
        } else if (value[0] == 'UNIT') {
            return 'System/' + action + '/' + value[1];
        } else if (value[0] == 'SYSTEM') {
            return 'EquipmentGroup/' + action + '/' + value[1];
        } else if (value[0] == 'EQUIPMENT_GROUP') {
            return 'Equipment/' + action + '/' + value[1];
        } 
        return;
    } else {
        if (value[0] == 'PLANT') {
            return 'Plant/' + action + '/';
        } else if (value[0] == 'AREA') {
            return 'Foc/' + action + '/';
        } else if (value[0] == 'UNIT') {
            return 'Unit/' + action + '/';
        } else if (value[0] == 'SYSTEM') {
            return 'System/' + action + '/';
        } else if (value[0] == 'EQUIPMENT_GROUP') {
            return 'EquipmentGroup/' + action + '/';
        } else if (value[0] == 'EQUIPMENT') {
            return 'Equipment/' + action + '/';
        }
        return '';
    }
    return '';
}

//REMOVE NODE FROM TREE
function RemoveItem(e) {
    var treeView = $("#TreeSBS").data("tTreeView");
    treeView.remove(e.node);
}

//ADD NEW NODE TO TREE
function AppendItem(text,value) {
    var treeView = $("#TreeSBS").data("tTreeView");
    var itemData = { Text: text ,Value:value, ImageUrl :"../Content/image/file.png"};
    var selected = GetNode('#TreeSBS .t-state-selected');
    treeView.append(itemData, selected.length != 0 ? selected : null);
    var select = treeView.findByText(text);
    //getSelectNode().removeClass("t-state-selected");
    //select.next('input').addClass("t-state-selected");
}

//GET NODE SELECTED FROM TREE
function GetNode(tree) {
    return $(tree).closest('li');
}

//CREATE CONTEXT MENU 
function CreateContextMenu(name) {
    return [
            {
                text: 'Edit',
                onclick: MyTreeView_OnEdit
            },
            {
                text: 'Delete',
                onclick: MyTreeView_OnDelete
            },
            {
                text: '+' + name,
                onclick: MyTreeView_OnAdd
            }
        ];
}

//SELECT NODE IN TREE
function SelectNode(value) {
    $("#TreeSBS").find('input.t-input[name="itemValue"][value="' + value + '"]').prev().addClass("t-state-selected");
    
}

//DESELECT NODE IN TREE
function DeselectNode() {
    $('#TreeSBS .t-state-selected').removeClass("t-state-selected");
}


//onSelect menu admin 2
function onSelectMenuAdmin2(e) {
    var item = $(e.item);
    $("#admin_header").html(item.find('> .t-link').text());
    // ======== Home ======= //
    if (item.find('> .t-link').text() == "Home") {
        $('#content_admin2').load('DashboardDefault/Home_Index');
    } else if (item.find('> .t-link').text() == "Profil Dit. Gas") {
        $('#content_admin2').load('DashboardDefault/Profil_Index');
    } else if (item.find('> .t-link').text() == "SHE - QMS") {
        $('#content_admin2').html('');
    }
    // ======== Data Bisnis ======= //
    else if (item.find('> .t-link').text() == "Dit. Gas") {
        $('#content_admin2').load('DataBisnisDitGasAdmin');
    }
    else if (item.find('> .t-link').text() == "JMG") {
        $('#content_admin2').html('');
    }
    else if (item.find('> .t-link').text() == "Anak Perusahaan") {
        $('#content_admin2').load('DataBisnisApAdmin');
    }
    else if (item.find('> .t-link').text() == "Afiliasi") {
        $('#content_admin2').load('DataBisnisAfiliasiAdmin');
    }
    // ======== SDM ======= //
    else if (item.find('> .t-link').text() == "SDM") {
        $('#content_admin2').html('');
    } else if (item.find('> .t-link').text() == "Aturan & Regulasi") {
        $('#content_admin2').html('');
    } else if (item.find('> .t-link').text() == "Kontak") {
        $('#content_admin2').html('');
    } else if (item.find('> .t-link').text() == "Kalender") {
        $('#content_admin2').html('');
    } else if (item.find('> .t-link').text() == "Lain - Lain") {
        $('#content_admin2').html('');
    }
}

//menu sdm
function onSelectMenuSdm(e) {
    var item = $(e.item);
    if (item.find('> .t-link').text() == 'Organisasi dan pekerja') {
        $('#content-sdm').load('Org/Index');
        $('#org').children('.t-link').css('background-color', '#FFA99A');
        $('#demografi').children('.t-link').css('background-color', '');
    } else if (item.find('> .t-link').text() == 'Demografi') {
        $('#content-sdm').load('DemografiSide');
        $('#demografi').children('.t-link').css('background-color', '#FFA99A');
        $('#org').children('.t-link').css('background-color', '');
    } /*else if (item.find('> .t-link').text() == 'Lahir vs Gol') {
        $('#content-sdm').load('Org/Index');
    } else if (item.find('> .t-link').text() == 'Lahir vs pend') {
        $('#content-sdm').load('Org/Index');
    } else if (item.find('> .t-link').text() == 'Gol vs pend') {
        $('#content-sdm').load('Org/Index');
    } else if (item.find('> .t-link').text() == 'Pensiun vs Gol') {
        $('#content-sdm').load('Org/Index');
    } else if (item.find('> .t-link').text() == 'Pensiun vs pend') {
        $('#content-sdm').load('Org/Index');
    } else if (item.find('> .t-link').text() == 'Teknisi Pensiun') {
        $('#content-sdm').load('Org/Index');
    } else if (item.find('> .t-link').text() == 'Sisa Teknisi') {
        $('#content-sdm').load('Org/Index');
    }*/
}

function onLoadMenuSdm(e) {
    $('#org').children('.t-link').css('background-color', '#FFA99A');
    $('#demografi').children('.t-link').css('background-color', '');
}

function loadIframe(iframeName, url) {
    var $iframe = $('#' + iframeName);
    if ($iframe.length) {
        $iframe.attr('src', url);
        return false;
    }
    return true;
}

function loadImage(imageName, url) {
    var $image = $('#' + imageName);
    if ($image.length) {
        $image.attr('src', url);
        return false;
    }
    return true;
}