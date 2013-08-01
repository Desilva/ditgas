/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(f){(function(){function i(b,a,c){var d=b+"="+c+(a?"; "+a:"");document.cookie=d}function j(b){var a=b+"=";if(document.cookie.length>0){var c=document.cookie.indexOf(a);if(c!=-1){c+=a.length;var d=document.cookie.indexOf(";",c);if(d==-1)d=document.cookie.length;return document.cookie.substring(c,d)}}return""}var h=!0;f.attachEvent("onBeforeViewChange",function(b,a,c,d){if(h){h=!1;var e=j("scheduler_settings");if(e)return e=unescape(e).split("@"),e[0]=this.templates.xml_date(e[0]),
window.setTimeout(function(){f.setCurrentView(e[0],e[1])},1),!1}var g=escape(this.templates.xml_format(d||a)+"@"+(c||b));i("scheduler_settings","expires=Sun, 31 Jan 9999 22:00:00 GMT",g);return!0});var g=f._load;f._load=function(){var b=arguments;if(!f._date&&f._load_mode){var a=this;window.setTimeout(function(){g.apply(a,b)},1)}else g.apply(this,b)}})()});
