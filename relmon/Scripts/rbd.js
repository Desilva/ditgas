function goTo(url)
{
	window.location = url;
}

function loadDetail(url) {
    //$('#rab_checker_unit').css("display", "block");
    $('#contentDetail').load(url, function () {
        //$('#rab_checker_unit').css("display", "none");
    });
}
   
$(document).ready(function(){
    $('.box, .box-kuning, .box-merah, .C3-Hijau, .C31-Hijau, .E3-Hijau, .F2-Hijau, .F3-Hijau, .V-Hijau, .Co-Hijau, .Co-Kuning, .Co-Merah, .Cp-Hijau, .C-Hijau, .Cb-Hijau, .Cb-Kuning, .Cb-Merah, .Ct-Hijau, .Cg-Hijau, .D-Hijau, .Ea-Hijau, .E-Hijau, .Ek-Hijau, .F-Hijau, .G-Hijau, .H-Hijau, .K-Hijau, .K-Kuning, .K-Merah, .box-big,  .box-big-kuning,  .box-big-merah, .D-Merah, .D-Kuning, .G-Kuning, .G-Merah, .Co-Kuning, .Co-Merah, .H-Kuning, .H-Merah, .Cg-Kuning, .Cg-Merah, .Ea-Kuning, .Ea-Merah, .Ct-Kuning, .Ct-Merah, .Cp-Kuning, .Cp-Merah, .E-Kuning, .E-Merah, .F-Kuning, .F-Merah, .C3-Kuning, .C31-Kuning, .E3-Kuning, .F2-Kuning, .F3-Kuning, .V-Kuning, .C3-Merah, .C31-Merah, .E3-Merah, .F2-Merah, .F3-Merah, .V-Merah').mouseover(function () {
		$(this).find('.box-value').show();
	}).mouseout(function(){
		$(this).find('.box-value').hide();
	});
});