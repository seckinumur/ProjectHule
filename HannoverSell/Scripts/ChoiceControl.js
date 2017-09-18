document.addEventListener('DOMContentLoaded', function () {

    $('#SK').selectpickerAjax({
        ajax: '/Admin/SinifKoduAjax/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#ST').selectpickerAjax({
        ajax: '/Admin/SinifTanimiAjax/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    //section control

    $('#S1').selectpickerAjax({
        ajax: '/Admin/Section1/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S2').selectpickerAjax({
        ajax: '/Admin/Section2/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S3').selectpickerAjax({
        ajax: '/Admin/Section3/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S4').selectpickerAjax({
        ajax: '/Admin/Section4/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S5').selectpickerAjax({
        ajax: '/Admin/Section5/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S6').selectpickerAjax({
        ajax: '/Admin/Section6/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S7').selectpickerAjax({
        ajax: '/Admin/Section7/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S8').selectpickerAjax({
        ajax: '/Admin/Section8/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S9').selectpickerAjax({
        ajax: '/Admin/Section9/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S10').selectpickerAjax({
        ajax: '/Admin/Section10/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S11').selectpickerAjax({
        ajax: '/Admin/Section11/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S12').selectpickerAjax({
        ajax: '/Admin/Section12/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S13').selectpickerAjax({
        ajax: '/Admin/Section13/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S14').selectpickerAjax({
        ajax: '/Admin/Section14/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#S15').selectpickerAjax({
        ajax: '/Admin/Section15/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });

    $('#MBA').selectpickerAjax({
        ajax: '/Admin/MusteriBulAjax/',
        ajaxPreProcess: function (res, cb) {
            console.log('process the response');
            return res;
        }
    });
});