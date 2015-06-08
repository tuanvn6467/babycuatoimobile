
function loadDefault()
{
    selected();
    checked();
	datetime('datetime', 'VN');
	scrollTop('onTop');

	//close panel
	$('.full').click(function () {
	    $('.full').hide();
	    $('.panel').hide();
	});

    //hide menu emtry
	$('.menu').each(function (e) {
	    if ($(this).find('li').size() == 0)
	        $(this).hide();
	});
	$('.menu h3').click(function () {
	    if ($(this).hasClass('hide'))
	        $(this).removeClass('hide');
        else
	        $(this).addClass('hide');
	    $(this).parent().find('ul').slideToggle();
	});
}


//tao cookie
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

//doc cookie
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return "";
}

//xoa bo cookie
function eraseCookie(name) {
    createCookie(name, "", -1);
}

function UrlSeo(tmp) {
    if (!tmp) return '';
    var str = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴqwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
    str += "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYqwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
    var arr = tmp.split("");
    for (var i = 0; i < arr.length; i++) {
        if (str.indexOf(arr[i]) < 0) {
            arr[i] = "-";
        }
    }

    tmp = ReplaceSpace(arr.join("").toLowerCase());
    if (tmp.indexOf("-") == 0)
        tmp = tmp.substring(1, tmp.length);
    if (tmp.lastIndexOf("-") == tmp.length - 1)
        tmp = tmp.substring(0, tmp.length - 1);
    return tmp;
}

function ReplaceSpace(str) {
    if (str.indexOf("--") > -1) {
        str = str.replace(/--/g, '-');
        return ReplaceSpace(str);
    }
    return str;
}

function selected()
{
    $("select").each(function () {
        var title = $(this).attr("title");
        if(title != null)
        {
            title = title.split(',');
            $(this).find("option").each(function () {
                for (i = 0; i < title.length; i++)
                    if ($(this).val() == title[i])
                        $(this).attr("selected", "selected");
                });
        }
    });
	//$('html, body').animate({scrollTop: $(document).height()}, 1000).animate({scrollTop: '0px'}, 500);
}

function checked() {
    $('input[type="radio"], input[type="checkbox"]').each(function () {
        var title = $(this).attr("title");
        var value = $(this).val();
        if (title != null && title.indexOf(value) >= 0)
            $(this).attr("checked", true);
    });
}

function scrollTop(where)
{
	$('#'+where).click(function() {
		$('html, body').animate({scrollTop: '0px'}, 500);
		return false;
	});
}

function editorBasic(id)
{
    var editor = CKEDITOR.replace(id,
	{
	    language: 'vi',
        width:"95%",
		toolbar :
		[
			 ['Source', '-', 'Bold', 'Italic', 'Underline', 'Strike','PasteFromWord'],
             ['Link', 'Unlink'],
			 ['NumberedList','BulletedList','-','Outdent','Indent'],
			 ['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
			 ['TextColor', 'BGColor'], ['FontSize'],
             ['Maximize', 'ShowBlocks']
		]
	});
}

function datetime(where, lang) {
    var date = new Date();
    var dd = date.getDate();
    var mm = date.getMonth();
    var yyyy = date.getFullYear();
    var yy = date.getYear();
    var day = date.getDay();
    
    var day_vn = new Array("Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bẩy");
    var moth_vn = mm + 1;
    var day_en = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
    var month_en = new Array("January", "February", "March", "April", "May", "June", "August", "September", "October", "November", "December");

    if (lang == 'VN')
        $('#' + where).append(day_vn[day] + ', ' + dd + '/' + moth_vn + '/' + yyyy);
    else if (lang == 'EN')
        $('#' + where).append(day_en[day] + ', ' + month_en[mm] + ' ' + dd + ', ' + yyyy);
}

function SearchForm() {
    $('input:checkbox[id="ckCheckAll"]').click(function () {
        $('input:checkbox[title^='+$(this).val()+']').prop('checked', this.checked);
                $('input:checkbox[id="ckCheckAll"]').prop('checked', this.checked);
//        if ($(this).is(':checked')) {
//            $('input:checkbox[name^="ckID"]').prop('checked', true);
//        }
//        else {
//            $('input:checkbox[name^="ckID"]').prop('checked', false);
//        }
    });
    
    $('input:checkbox[id="ckCheckAll1"]').click(function () {
        $('input:checkbox[title^=' + $(this).val() + ']').attr('checked', this.checked);
        $('input:checkbox[id="ckCheckAll1"]').attr('checked', this.checked);
    });

    $('select[id="show"],select[id="page"]').change(function () {
        window.location.href = $(this).find("option:selected").attr("title");
    });

    $('.table-list input,.table-list select').change(function () {
        if($(this).attr('title')!='ckID')
            $(this).parent().parent().find('input[title="ckID"]').attr('checked',true);
    });
}

function CheckAllPermission() {
    $('input[id="ckPermission"]').change(function () {
        var name = $(this).attr("name");
        $('input[lang="' + name + '"]').attr("checked", this.checked);
        $('input[lang="' + name + '"]').trigger('change');
    });
}

function ActionForm(action)
{
    $('button[id="btnActionAdd"]').click(function () {
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/AddNew");
        //$('input:checkbox[title="ckID"]').attr('checked', true);
        $('#frmList').submit();
    });

    $('button[id="btnActionUpdate"]').click(function () {
        if (window.confirm("Bạn có chắc chắn cập nhật không?")) {
            $('#frmList').attr('method', "POST");
            $('#frmList').attr('action',action+"/Update");
            $('#frmList').submit();
        }
    });
    $('button[id="btnActionImport"]').click(function () {
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/ImportUpdate");
        $('input:checkbox[title="ckID"]').attr('checked', true);
        //alert($('#frmList').attr('action'));
        $('#frmList').submit();
    });
    //$('button[id="btnActionXuatKhoNB"]').click(function () {
    //    $('#frmList').attr('method', "POST");
    //    $('#frmList').attr('action', action + "/ImportUpdate");
    //    $('input:checkbox[title="ckID"]').attr('checked', true);
    //    //alert($('#frmList').attr('action'));
    //    $('#frmList').submit();
    //});
    $('button[id="btnActionExport"]').click(function () {
        //var selectedOption = $("#slSearchType option:selected").text();
        //if (selectedOption == null) $("#slSearchType option:1").text();
        //$("<input/>", { type: 'hidden', name: 'hdfInventoryName' }).val(selectedOption).appendTo("#frmList");
        //alert(selectedOption);
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/ExportData");
        //$('input:checkbox[title="ckID"]').attr('checked', true);
        //alert($('#frmList').attr('action'));
        $('#frmList').submit();
    });

    $('button[id="btnExportBC"]').click(function () {
        //var selectedOption = $("#slSearchType option:selected").text();
        //if (selectedOption == null) $("#slSearchType option:1").text();
        //$("<input/>", { type: 'hidden', name: 'hdfInventoryName' }).val(selectedOption).appendTo("#frmList");
        //alert(selectedOption);
        $('#frmSearch').attr('method', "POST");
        $('#frmSearch').attr('action', action + "/ExportData");
        //$('input:checkbox[title="ckID"]').attr('checked', true);
        //alert($('#frmList').attr('action'));
        $('#frmSearch').submit();
    });

    $('button[id="btnChon"]').click(function () {
        //var selectedOption = $("#slSearchType option:selected").text();
        //if (selectedOption == null) $("#slSearchType option:1").text();
        //$("<input/>", { type: 'hidden', name: 'hdfInventoryName' }).val(selectedOption).appendTo("#frmList");
        //alert(selectedOption);
        $('#frmSearch').attr('method', "GET");
        $('#frmSearch').attr('action', action + "/Index");
        //$('input:checkbox[title="ckID"]').attr('checked', true);
        //alert($('#frmList').attr('action'));
        $('#frmSearch').submit();
    });

    $('button[id="btnActionUpdatePermission"]').click(function () {
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/UpdatePermission");
        $('#frmList').submit();
    });

    $('button[id="btnActionDelete"]').click(function () {
        if ($('input:checkbox[title="ckID"]:checked').val() != null) {
            if (confirm("Bạn thực sự muốn xóa?")) {
                $('#frmList').attr('method', "POST");
                $('#frmList').attr('action', action + "/Delete");
                $('#frmList').submit();
            }
        }
    });

    $('button[id="btnDelete"]').click(function () {
        if ($('input:checkbox[title="ckID"]:checked').val() != null) {
            if (confirm("Bạn thực sự muốn xóa?")) {
                $('#frmList').attr('method', "POST");
                $('#frmList').attr('action', action + "/DeleteList");
                $('#frmList').submit();
            }
        }
    });
    $('button[id="btnUpdate"]').click(function () {
        $('#frmAdd').attr('method',"POST");
        $('#frmAdd').attr('action',action+"/Create");
        $('#frmAdd').submit();
    });

   
    
    //sln added
    $('button[id="btnTemplate"]').click(function () {
        $('#frmTemplate').attr('method', "POST");
        $('#frmTemplate').attr('action', action + "/CreateTemplate");
        $('#frmTemplate').submit();
    });

    $('button[id="btnStyle"]').click(function () {
        $('#frmStyle').attr('method', "POST");
        $('#frmStyle').attr('action', action + "/CreateStylesheet");
        $('#frmStyle').submit();
    });
    
    $('button[id="btnAddVas"]').click(function () {
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/AddNew");
        $('#frmList').submit();
    });
    $('button[id="btnUpdateVas"]').click(function () {
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/Update");
        $('#frmList').submit();
    });
    $('button[id="btnVasDelete"]').click(function () {
        if ($('input:checkbox[title="ckID"]:checked').val() != null) {
            if (confirm("Bạn thực sự muốn xóa?")) {
                $('#frmList').attr('method', "POST");
                $('#frmList').attr('action', action + "/Delete");
                $('#frmList').submit();
            }
        }
    });
    //btnReload
    $('button[id="btnReload"]').click(function () {
        $('#frmList').attr('method', "POST");
        $('#frmList').attr('action', action + "/UpdateInventory");
        $('#frmList').submit();
    });
}

function Delete(id) {
    $('input:checkbox[value="' + id + '"]').attr('checked', true);
    $('#btnActionDelete').trigger('click');
}

function DeleteVas(id) {
    $('input:checkbox[value="' + id + '"]').attr('checked', true);
    $('#btnVasDelete').trigger('click');
}

function DeleteList(id) {
    $('input:checkbox[value="' + id + '"]').attr('checked', true);
    $('#btnDelete').trigger('click');
}

function ShowUpload() {
    $('button[id="btnImage"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "../file");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
    });

    $('input[id="txtImage"]').dblclick(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "/webcms/file");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
        createCookie("cookieFileValue", $(this).val(), 1);
    });
}

//function Xuat() {
//    $('button[id="btnXuat"]').click(function () {
//        $('#frmAdd').attr('method', "POST");
//        $('#frmAdd').attr('action', action + "/ExportExcelDoanhThu");
//        $('#frmAdd').submit();
//    });
//}

function Xuat() {
    $('button[id="btnXuat"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "ExportExcelDoanhThu");
        //window.open("InventoryDetail/ImportProduct", "xxx", "height=300,width=400");
    });
}

function ImageDescription(sName,sSrc) {
    var oEditor = CKEDITOR.instances.txtContent;
    var src = "/uploads/" + sSrc;
    if (oEditor.mode == 'wysiwyg')
        oEditor.insertHtml('<a title="' + sName + '" href="' + src + '" class="slideshow-list" rel="slideshow-list"><img alt="' + sName + '" src="' + src + '" /></a>');
}
function ImageFile(sSrc) {
    $('#txtImage').val(sSrc);
}
function ImageList(sName, sImg) {
    var img = $('#txtSlideImage');
    if (img.val() == '')
        img.val(img.val() + sName+'\n');
    else
        img.val(img.val() + '\n' + sName+'\n');
    img.val(img.val() + sImg);
}

function ShowSearch() {
    $('#btnAddRelated').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "/webcms/poll/search");
        createCookie("cookieSearchBack", $(this).attr("lang"), 1);
        createCookie("cookieSearchValue", $(this).val(), 1);
    });

    $('input[id="txtPoll"]').dblclick(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "/webcms/poll/search");
        createCookie("cookieSearchBack", $(this).attr("lang"), 1);
        createCookie("cookieSearchValue", $(this).val(), 1);
    });
}
function SetPoll(ID) {
    $('#txtPoll').val(ID);
}
function SetRelated(ID) {
    if($('#txtRelated').val()=='')
        $('#txtRelated').val(ID);
    else
        $('#txtRelated').val($('#txtRelated').val()+','+ID);
}


function ShowEditor()
{
    $('button[id="btnPanel"]').click(function () {
        $('.full').show();
        $('.' + $(this).attr('title')).show();

        var oEditor = CKEDITOR.instances.txtEditor;
        var value = $('#' + $(this).attr('lang')).val();
        oEditor.setData(value);
    });
}

function CreateGroup(where, arr) {
    $.each(arr, function (val, text) {
        $('#' + where).append(new Option(text, val));
    });
}

function SetValue(value, arr, where) {
    $('#' + where + ' option').remove();
    $.each(arr, function (val, text) {
        if (val.indexOf(value + '#') >= 0) {
            var id = val.substr(value.length + 1, val.length - value.length);
            $('#' + where).append(new Option(text, id));
        }
    });
}

function getCateName(child, parent, cateVal, where) {
    if (cateVal != '') {
        $.each(child, function (val, text) {
            if (val.indexOf(cateVal) >= 0) {
                var per = val.substr(0, val.indexOf("#"));
                $.each(parent, function (val2, text2) {
                    if (val2.indexOf(per) >= 0) {
                        $('#' + where).val(text2 + " " + text);
                        return;
                    }
                });
            }
        });
    }
}

function loadTabs(id) {
    //When page loads...
    $("#" + id + " .tab_content").hide(); //Hide all content
    $("#" + id + " ul.tabs li:first").addClass("active").show(); //Activate first tab
    $("#" + id + " .tab_content:first").show(); //Show first tab content

    //On Click Event
    $("#" + id + " ul.tabs li").click(function () {

        $("#" + id + " ul.tabs li").removeClass("active"); //Remove any "active" class
        $(this).addClass("active"); //Add "active" class to selected tab
        $("#" + id + " .tab_content").hide(); //Hide all tab content

        var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
        $(activeTab).fadeIn(); //Fade in the active ID content
        return false;
    });
}

//sonln added 2013oct18
function ShowUpload1() {
    $('button[id="btnImage"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "../Product/ProductSearch");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
    });

    $('button[id="btnBarcode"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "../Product/ProductSearchBarcode");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
        eraseCookie("barcode_search");
    });
}
function ImportProduct() {
    $('button[id="btnImport"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "InventoryDetail/ImportProduct");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
        //window.open("InventoryDetail/ImportProduct", "xxx", "height=300,width=400");
    });
}

function ImportProductSX() {
    $('button[id="btnImport"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "NhapKhoSX/ImportProduct");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
        //window.open("InventoryDetail/ImportProduct", "xxx", "height=300,width=400");
    });
}

function XuatKhoNB() {
    $('button[id="btnImport"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "XuatKhoNB/ImportProduct");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
        //window.open("InventoryDetail/ImportProduct", "xxx", "height=300,width=400");
    });
}

function ImportExcel() {
    $('button[id="btnImportProduct"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "Product/ImportExcel");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
    });
}
function ExportExcel() {
    $('button[id="btnExportProduct"]').click(function () {
        $('.full').show();
        $('.upload').show();
        $('#fUpload').attr("src", "Product/ExportExcel");
        createCookie("cookieFileBack", $(this).attr("lang"), 1);
    });
}



//kiem tra browser cho dung cookie khong
function checkBrowserEnableCookie() {
    var cookieEnabled = (navigator.cookieEnabled) ? true : false

    //if not IE4+ nor NS6+
    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
        document.cookie = "testcookie";
        cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false
    }

    if (cookieEnabled) return true;
    else return false;
}

//tao cookie
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

//doc cookie
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return "";
}

//xoa bo cookie
function eraseCookie(name) {
    createCookie(name, "", -1);
}

//doc so luong san pham dang co trong gio hang
function countShoppingCart(name) {
    //tao cookie gio hang neu cua co
    if (readCookie(name) == "") {
        document.getElementById('count_shopping_cart').innerHTML = "<b>(0)</b>";
    } else {
        //add gia tri vao shopping cart
        var current_cart = readCookie(name);
        var ca = current_cart.split('$$');
        number_product = ca.length - 1;
        document.getElementById('count_shopping_cart').innerHTML = "<b>(" + number_product + ")</b>";
    }
}

//them san pham vao gio hang
function addToShoppingCart(productId, quantity, discount) {
    //tao cookie gio hang neu cua co
    if (readCookie('quick_shopping_cart') == null) {
        createCookie('quick_shopping_cart', '', 1);
    }
    //add gia tri vao shopping cart
    var current_cart = readCookie('quick_shopping_cart');

    //kiem tra xem da duoc them vao cart hay chua
    if (current_cart.search('\\$\\$' + productId) == -1) {
        var new_cart = current_cart + '$$' + productId + '#' + quantity + '#' + discount;
        createCookie('quick_shopping_cart', new_cart, 1);
        //doc lai gia tri moi
        //countShoppingCart('quick_shopping_cart');
        return 1;
    } else {
//        alert('Sản phẩm này đã có trong giỏ hàng \nVui lòng xem chi tiết giỏ hàng để chỉnh sửa hoặc xóa bớt sản phẩm!');
        var res = current_cart.split('$$');
        var quant = 0;
        for (var i = 0; i < res.length; i++) {
            var item = res[i].split('#');
            item[1] = parseInt(item[1]) + 1;
            quant = item[1];
            res[i] = item.join('#');
        }
        current_cart = res.join('$$');
        createCookie('quick_shopping_cart', current_cart, 1);
        return quant;
    }
}

function addToShoppingCart1(productId, quantity, discount) {
    //tao cookie gio hang neu cua co
    if (readCookie('quick_shopping_cart') == null) {
        createCookie('quick_shopping_cart', '', 1);
    }
    //add gia tri vao shopping cart
    var current_cart = readCookie('quick_shopping_cart');

    //kiem tra xem da duoc them vao cart hay chua
    if (current_cart.search('\\$\\$' + productId) == -1) {
        var new_cart = current_cart + '$$' + productId + '#' + quantity + '#' + discount;
        createCookie('quick_shopping_cart', new_cart, 1);
        //doc lai gia tri moi
        //countShoppingCart('quick_shopping_cart');
        return 0;
    } else {
        //        alert('Sản phẩm này đã có trong giỏ hàng \nVui lòng xem chi tiết giỏ hàng để chỉnh sửa hoặc xóa bớt sản phẩm!');
        var res = current_cart.split('$$');
        var quant = 0;
        for (var i = 0; i < res.length; i++) {
            var item = res[i].split('#');
            if (item[0] == productId) {
                item[1] = parseInt(item[1]) + parseInt(quantity);
                quant = item[1];
            }
            res[i] = item.join('#');
        }
        current_cart = res.join('$$');
        createCookie('quick_shopping_cart', current_cart, 1);
        return quant;
    }
}

function addToVasCart1(productId) {
    //tao cookie gio hang neu cua co
    if (readCookie('quick_vas_cart') == null) {
        createCookie('quick_vas_cart', '', 1);
    }
    //add gia tri vao shopping cart
    var current_cart = readCookie('quick_vas_cart');

    //kiem tra xem da duoc them vao cart hay chua
    if (current_cart.search('\\$\\$' + productId) == -1) {
        var new_cart = current_cart + '$$' + productId;
        createCookie('quick_vas_cart', new_cart, 1);
        return 1;
    } else {
        alert('Dịch vụ này đã có \nVui lòng chọn dịch vụ khác!');
    }
}


//Thêm sản phẩm giỏ hàng và chuyển tới trang giỏ hàng
function addToCartRedirect(productId, quantity, discount) {
    if (readCookie('quick_shopping_cart') == null) {
        createCookie('quick_shopping_cart', '', 1);
    }
    var current_cart = readCookie('quick_shopping_cart');

    if (current_cart.search('\\$\\$' + productId) == -1) {
        var new_cart = current_cart + '$$' + productId + '#' + quantity + '#' + discount;
        createCookie('quick_shopping_cart', new_cart, 1);
        countShoppingCart('quick_shopping_cart');
        window.location = "/order";
    } else {
        alert('Sản phẩm này đã có trong giỏ hàng \nVui lòng xem chi tiết giỏ hàng để chỉnh sửa hoặc xóa bớt sản phẩm!');
        window.location = "/order";
    }
}

function checkItemInCart(productId, type) {
    var current_cart = readCookie('quick_shopping_cart');
    //kiem tra xem da duoc them vao cart hay chua
    if (current_cart.search('\\$\\$' + productId) != -1) {
        return 'Đang trong giỏ hàng!';
    }
}

//xoa bo item trong cart
function deleteFromCart(productId) {
    if (confirm('Bỏ mua sản phẩm này khỏi giỏ hàng? ')) {
        var current_cart = readCookie('quick_shopping_cart');
        var newCart = current_cart.split('$$');
        for (i = 0; i < newCart.length; i++)
        {
            var str = newCart[i].split("#");
            if (str[0]==productId)
                newCart.splice(i, 1);
        }
        var newC = newCart.join("$$");
        createCookie('quick_shopping_cart', newC, 1);
    }
}

function deleteFromVas(productId) {
    if (confirm('Hủy dịch vụ này khỏi hóa đơn? ')) {
        var current_cart = readCookie('quick_vas_cart');
        var newCart = current_cart.split('$$');
        for (i = 0; i < newCart.length; i++) {
            var str = newCart[i].split("#");
            if (str[0] == productId)
                newCart.splice(i, 1);
        }
        var newC = newCart.join("$$");
        createCookie('quick_vas_cart', newC, 1);
    }
}

//cap nhat so luong san pham trong cart

function updateCart(productId, quantity, discount) {
    //var newquantity = document.getElementById("item_" + productId).value;
    var newquantity = parseInt(quantity);
    var newdiscount = parseInt(discount);
    if (newquantity > -1) {
        if (newquantity < 1) {
            //Nếu cập nhật số lượng < 1 thì coi như là xóa sản phẩm khỏi cart
            deleteFromCart(productId, newquantity, discount);
            DeleteDetail(productId);
        } else {
            var current_cart = readCookie('quick_shopping_cart');
            var newCart = current_cart.split('$$');
            for (i = 0; i < newCart.length; i++) {
                var str = newCart[i].split("#");
                if (str[0] == productId)
                    str[1] = newquantity;
                newCart[i] = str.join("#");
            }
            var newC = newCart.join("$$");
            createCookie('quick_shopping_cart', newC, 1);
        }
    }
    checkPId(productId);
    if (newdiscount > -1) {
        var current_cart = readCookie('quick_shopping_cart');
        var newCart = current_cart.split('$$');
        for (i = 0; i < newCart.length; i++) {
            var str = newCart[i].split("#");
            if (str[0] == productId)
                str[2] = newdiscount;
            newCart[i] = str.join("#");
        }
        var newC = newCart.join("$$");
        createCookie('quick_shopping_cart', newC, 1);
    }
}

function checkPId(id) {
    var quantity = parseInt(document.getElementsByName("txtQuantity-" + id)[0].value.replace(",",""));
    var price = parseInt(document.getElementsByName("txtPrice-" + id)[0].value.replace(/[^0-9\.]+/g, ""));
    var discount = parseInt(document.getElementsByName("txtDiscount-" + id)[0].value.replace(",", ""));
    var cash = 0;
    if (!isNaN(quantity) && !isNaN(discount)) {
        if (discount < 0) {
            discount = Math.abs(discount);
            $("input[name='txtDiscount-" + id + "']").val(discount);
        }
        if (quantity >= 0) {
//            if (quantity == 0)
//                quantity = 1;
            if (discount < 100) {
                cash = quantity * ((price * (100 - discount)) / 100);
            } else {
                cash = quantity * (price - discount);
            }
        } else {
            discount = 0;
            $("input[name='txtDiscount-" + id + "']").val(discount);
            cash = quantity * price;
        }
        
    } else {
        if (isNaN(quantity)) {
            quantity = 1;
            $("input[name='txtQuantity-" + id + "']").val(quantity);
        }
        if (isNaN(discount)) {
            discount = 0;
            $("input[name='txtDiscount-" + id + "']").val(discount);
        }
        cash = quantity * price;
    }
    //document.getElementsByName("txtCash-" + id).value = addCommas(cash);
    $("input[name='txtCash-" + id + "']").val(addCommas(cash));
    //alert(addCommas(cash));
    updateOrder();
}

function updateOrder() {
    var current_cart = readCookie('quick_shopping_cart');
    var newCart = current_cart.split('$$');
    var sumCash = 0;
    var sumCash1 = 0;
    for ( var i = 1; i < newCart.length; i++) {
        var str = newCart[i].split("#");
        var curProductCash = $("#txtCash-" + str[0] + "").val();
        if (curProductCash == "") curProductCash = "0";
        var curProductPrice = $("#txtPrice-" + str[0] + "").val();
        if (curProductPrice == "") curProductPrice = "";
        var curProductQuant = $("#txtQuantity-" + str[0] + "").val();
        if (curProductQuant == "") curProductPrice = "0";
        if (parseInt(curProductQuant) > 0) {
            sumCash = sumCash + parseInt(curProductCash.replace(/[^0-9\.]+/g, ""));
            sumCash1 = sumCash1 + (parseInt(curProductPrice.replace(/[^0-9\.]+/g, "")) * parseInt(curProductQuant)) - parseInt(curProductCash.replace(/[^0-9\.]+/g, ""));
        } else {
            sumCash = sumCash - parseInt(curProductCash.replace(/[^0-9\.]+/g, ""));
        }
    }
    var n = $("input[name^='txtCash-']").size();
    for (i = 1; i <= n; i++) {
        curProductCash = $("input[name='txtCash-" + i + "']").val();
        if (typeof(curProductCash) !== "undefined") {
            curProductPrice = $("input[name='txtPrice-" + i + "']").val();
            curProductQuant = $("input[name='txtQuantity-" + i + "']").val();
            if (parseInt(curProductQuant) > 0) {
                sumCash = sumCash + parseInt(curProductCash.replace(/[^0-9\.]+/g, ""));
                sumCash1 = sumCash1 + (parseInt(curProductPrice.replace(/[^0-9\.]+/g, "")) * parseInt(curProductQuant)) - parseInt(curProductCash.replace(/[^0-9\.]+/g, ""));
            }
            else{
                sumCash = sumCash - parseInt(curProductCash.replace(/[^0-9\.]+/g, ""));
            }
        }
    }
    
    $("input[name='orderCash']").val(addCommas(sumCash));
    $("input[name='itemDiscountSum']").val(addCommas(sumCash1));
    var orderDiscount = $("input[name='txtDiscount']").val();
    if (orderDiscount == "") orderDiscount = "0";
    var currentCart = readCookie('quick_vas_cart');
    var lstVas = currentCart.split("$$");
    for (i = 1; i < lstVas.length; i++) {
        var vasCasVal = $("input[name='txtVasCash-" + lstVas[i] + "']").val();
        var vasCas = 0;
        if (vasCasVal != ""){
            vasCas = parseInt(vasCasVal.replace(/,/g, ""));
        }
        sumCash = sumCash + vasCas;
    }
    n = $("input[name^='txtVasCash-']").size();
    for (i = 1; i <= n; i++) {
        vasCasVal = $("input[name='txtVasCash-" + i + "']").val();
        if (typeof vasCasVal !== "undefined") {
            vasCas = parseInt(vasCasVal.replace(/,/g, ""));
            sumCash = sumCash + vasCas;
        }
    }
    sumCash = sumCash - parseInt(orderDiscount.replace(/,/g, ""));
    $("input[name='orderCash1']").val(addCommas(sumCash));
    var prePaid = $("input[name='prePayment']").val();
    if (prePaid == "") prePaid = "0";
    $("input[name='debtCash']").val(addCommas(sumCash - parseInt(prePaid.replace(/[^0-9\.]+/g, ""))));
    
}

function addCommas(n) {
    var rx = /(\d+)(\d{3})/;
    return String(n).replace(/^\d+/, function (w) {
        while (rx.test(w)) {
            w = w.replace(rx, '$1,$2');
        }
        return w;
    });
}
function priceCheck(price) {
    if (isInt(price.value)) {
        var iPrice = parseInt(price.value);
        if (iPrice == 0) {
            price.value = "";
        } else {
            price.value = iPrice;
        }
    } else {
        price.value = "0";
    }
}
function isInt(value) {
    return !isNaN(value) &&
            parseInt(Number(value)) == value &&
            (value + "").replace(/ /g, '') !== "";
}
