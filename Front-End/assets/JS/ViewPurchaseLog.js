$(document).ready(function(){

		$.ajax({
		url:"http://localhost:56213/api/purchase_log",
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var purchaseLog = "<table border='1' cellspacing='0'><tr><th>&nbsp Customer ID &nbsp</th><th>&nbsp Product Name &nbsp</th><th>&nbsp Product Description &nbsp</th><th>&nbsp Buying Price &nbsp</th><th>&nbsp Category &nbsp</th><th>&nbsp Brand &nbsp</th><th>&nbsp Features &nbsp</th><th>&nbsp Quantity &nbsp</th><th>&nbsp Purchased Date &nbsp</th></tr>";
				var purchaseLogData = xmlHttp.responseJSON;
				console.log(purchaseLogData.length);
				for (var i = 0; i < purchaseLogData.length; i++) {
					 purchaseLog += "<tr><td>"+purchaseLogData[i].customerId+"</td><td>"+purchaseLogData[i].productName+"</td><td>"+purchaseLogData[i].productDescription+"</td><td>"+purchaseLogData[i].buyingPrice+"</td><td>"+purchaseLogData[i].category+"</td><td>"+purchaseLogData[i].brand+"</td><td>"+purchaseLogData[i].features+"</td><td>"+purchaseLogData[i].quantity+"</td><td>"+purchaseLogData[i].purchasedDate+"</td></tr>";
				}
				purchaseLog += "</table>"
				$("#showPurchaseLog").html(purchaseLog);
			}
			else
			{
				$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
			}
		}

		});

});