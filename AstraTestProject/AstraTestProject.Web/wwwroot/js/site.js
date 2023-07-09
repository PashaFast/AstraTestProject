$(document).ready(function () {
	let data = {
		datatype: "json",
		datafields: [{
			name: 'orderDate'
		},
		{
			name: 'orderNumber'
		},
		{
			name: 'customerName'
		},
		{
			name: 'carModel'
		},
		{
			name: 'amount'
		},
		{
			name: 'carCost'
		},
		{
			name: 'totalCost'
		},
		],
		id: 'id',
		url: "https://localhost:44379/api/Home/GetAllOrders",
	};

	$("#jqxgrid").jqxGrid({
		width: 750,
		pageable: true,
		source: data,
		columnsresize: true,
		autoheight: true,
		columns: [{
			text: 'Order Date',
			datafield: 'orderDate',
			width: 100
		},
		{
			text: 'Order Number',
			datafield: 'orderNumber',
			width: 100
		},
		{
			text: 'Customer Name',
			datafield: 'customerName',
			width: 150
		},
		{
			text: 'Car Model',
			datafield: 'carModel',
			width: 100
		},

		{
			text: 'Amount',
			datafield: 'amount',
			width: 100
		},
		{
			text: 'Car Cost',
			datafield: 'carCost',
			width: 100
		},
		{
			text: 'Total Cost',
			datafield: 'totalCost',
			width: 100
		},
		],
	});


	const hubConnection = new signalR.HubConnectionBuilder()
		.withUrl("https://localhost:44379/chat")
		.build();

	$('#jqxgrid').on('rowselect', function (event) {
		let orderNumber = event.args.row.orderNumber.toString();

		let selectedRowIndex = event.args.rowindex;
		let unselectedRowIndex = -1;

		let indexes = $('#jqxgrid').jqxGrid('getselectedrowindexes');

		if (indexes.length != 0) {
			unselectedRowIndex = indexes[0];
		}

		if (selectedRowIndex != unselectedRowIndex) {
			hubConnection.invoke("Send", orderNumber, selectedRowIndex)
				.catch(function (err) {
					return console.error(err.toString());
				});
		}

	});

	hubConnection.on("Receive", function (orderNumber, selectedRowIndex) {

		$("#selectedOrderNumber").empty();

		let messageElement = $("<span>", { text: `Номер выбранного заказа: ${orderNumber}.` });
		$("#selectedOrderNumber").append(messageElement);

		$('#jqxgrid').jqxGrid('selectrow', selectedRowIndex);

	});

	hubConnection.start()
		.catch(function (err) {
			return console.error(err.toString());
		});


});