var counter = 1;
var oneResult;

function send(){
	if (msgDiv == null) {
		document.body.innerHTML = "";
		msgDiv = addTo("div",document.body);
	}
	//window.location = "localhost:8080";
	/*
	var id = setInterval(function(){
		Rq("http://localhost:8080?name=bing&" + FormMessage());
		//console.log("counter % 20 = " + (counter % 20));
		if (!(counter % 3)) {
			clearInterval(id);
		}
	},200);*/
	Rq("http://localhost:8080?name=bing&" + FormMessage_0());
	Rq("http://localhost:8080?name=bing&" + FormMessage_1());
	Rq("http://localhost:8080?name=bing&" + FormMessage_2());
	Rq("http://localhost:8080?name=bing&" + FormMessage_3());
}

function Rq(url) {
	var xhr = new XMLHttpRequest();
	xhr.open("Get",url,true);
	xhr.setRequestHeader("Access-Control-Allow-Origin","*");
	xhr.setRequestHeader("POWERED-BY-MENGXIANHUI", "Approve");  
	xhr.setRequestHeader("Content-Type", "application/xml");
	xhr.withCredentials = "true";  
	xhr.onreadystatechange = function(){
		if (xhr.readyState == 4) {
			//page[0] = JSON.parse(xhr.responseText.substr(8,7185));//(xhr.responseText);
			counter++;
			//console.log(JSON.parse(xhr.responseText));
			oneResult = (JSON.parse(xhr.responseText));
			//div.innerHTML = "<center>" + xhr.responseText + "</center>"
			dearWithResp(JSON.parse(xhr.responseText));
		}
	}
	xhr.send();
}

function RqAjax(targetUrl){
	xmlHttp = GetXmlHttpObject();
	xmlHttp.onreadystatechange = function(){
		if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
			var div = addTo("div",msgDiv);
			div.innerHTML = "<center>" + xhr.responseText + "</center>"
			if (counter % 2) {
				div.style.backgroundColor = "bisque"; 
			} else {
				div.style.backgroundColor = "aquamarine";
			}
			counter++;
		}
	}
	xmlHttp.open("GET",targetUrl,true);
	xmlHttp.send(null);
}

var msgDiv = null;

function addTo(type,father) {
	var ele = document.createElement(type);
	if (type == "td") {
		ele.style.border = "1px solid";
	}
	father.appendChild(ele);
	return ele;
}

function FormMessage_0(){
	var dic = {
		dbtype : 0,
		FileName : "D:\\\\Data.xls"
	};
	return "connect=" + JSON.stringify(dic) + "&sql=select * from [Sheet1$]";
}

function FormMessage_1(){
	var dic = {
		dbtype : 1,
		FileName : "D:\\\\Data.mdb"
	};
	return "connect=" + JSON.stringify(dic) + "&sql=select * from test";
}

function FormMessage_2(){
	var dic = {
		dbtype : 2,
		FileName : "D:\\\\NET_DISK_FILE_SYSTEM_r.mdf"
	};
	return "connect=" + JSON.stringify(dic) + "&sql=select * from nd_user";
}

function FormMessage_3(){
	var dic = {
		dbtype : 3,
		sqlSet : {
			isWindowsAuth : true,
			Server_Machine : "DESKTOP-ESTNB01\\SQLEXPRESS",
			Database : "net_disk"
		}
	};
	return "connect=" + JSON.stringify(dic) + "&sql=select * from nd_log";
}

function dearWithResp(json){
	var Table = JSON.parse(oneResult.searchResult).Table;
	if (Table.length) {
		var div = addTo("div",msgDiv);
		if (counter % 2) {
			div.style.backgroundColor = "bisque"; 
		} else {
			div.style.backgroundColor = "aquamarine";
		}
		var table = addTo("table",div);
		var tr_head = addTo("tr",addTo("thead",table));
		////////////////////////////////////////////////////////
		var tbody = addTo("tbody",table);
		for (var i in Table[0]) {
			var td = addTo("td",tr_head);
			td.innerHTML = "<center>" + i + "</center>";
		}
		for (var i = 0;i < Table.length;i++) {
			var tr_body = addTo("tr",tbody);
			for (var j in Table[i]) {
				var td = addTo("td",tr_body);
				if (typeof Table[i][j] == "string") {
					td.innerHTML = "<center>" + Table[i][j].substr(0,100) + "</center>";
				} else {
					td.innerHTML = "<center>" + Table[i][j] + "</center>";
				}
			}
		}
	}
}

