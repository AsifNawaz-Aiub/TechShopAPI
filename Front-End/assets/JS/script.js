$(document).ready(function(){

	$.ajax({
		url:"http://localhost:56213/api/old_product",
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var oldProducts = "<table border='1' id='oldProductsList' cellspacing='0'><tr><th>Customer ID</th><th>Product Name</th><th>Product Description</th><th>Requested Price</th><th>Category</th><th>Brand</th><th>Features</th><th>Quantity</th><th>Options</th></tr>";
				var oldProductsData = xmlHttp.responseJSON;
				var accept = "Accept";
				var reject = "Reject";
				// console.log(oldProductsData.length);
				for (var i = 0; i < oldProductsData.length; i++) {
					if(oldProductsData[i].status == "Pending")
					 {
					 	oldProducts += "<tr><td>"+oldProductsData[i].customerId+"</td><td>"+oldProductsData[i].productName+"</td><td>"+oldProductsData[i].productDescription+"</td><td>"+oldProductsData[i].buyingPrice+"</td><td>"+oldProductsData[i].category+"</td><td>"+oldProductsData[i].brand+"</td><td>"+oldProductsData[i].features+"</td><td>"+oldProductsData[i].quantity+"</td>";
					 	oldProducts += "<td><a href='BuyingAgentHome.html?acceptId="+oldProductsData[i].id+"'class='linkBtn submitBtn'>"+accept+"</a><br><br><a href='BuyingAgentHome.html?rejectId="+oldProductsData[i].id+"'class='linkBtn logoutBtn''>"+reject+"</a></td><br></tr>";
					 }
				}
				oldProducts += "</table>"
				$("#showOldProducts").html(oldProducts);
			}
			else
			{
				$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
			}
		}


	});

    	var aId = GetParameterValues('acceptId');
    	var rId = GetParameterValues('rejectId');      
	    function GetParameterValues(param) {  
	        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');  
	        for (var i = 0; i < url.length; i++) {  
	            var urlparam = url[i].split('=');  
	            if (urlparam[0] == param) {  
	                return urlparam[1];  
	            }  
	        }  
	    }

	    if(aId)
	    {
	    	$.ajax({
			url:"http://localhost:56213/api/old_product/accept/"+aId,
			method:"POST",
			headers:"Content-Type:application/json",
			complete:function(xmlHttp,status){
				if(xmlHttp.status == 201)
				{
					window.location.href = "BuyingAgentHome.html";
					$("#msg").html("Item added in Purchase Log");	
				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}

			});
	    }
	    else if(rId)
	    {
	    	$.ajax({
			url:"http://localhost:56213/api/old_product/reject/"+rId,
			method:"PUT",
			headers:"Content-Type:application/json",
			complete:function(xmlHttp,status){
				if(xmlHttp.status == 200)
				{
					window.location.href = "BuyingAgentHome.html";
					$("#msg").html("Item rejected from Old Products");	
				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}

			});
	    }

});