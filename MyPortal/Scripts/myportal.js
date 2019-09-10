function formatDate(data) {
    var date = new Date(data);
    return moment(date).format("DD MMM YYYY");
}

function formatCurrency(data) {
    return "£" + data.toFixed(2).toString();
}

function refreshTable(tableName) {
    var grid = document.getElementById(tableName.toString()).ej2_instances[0];
    grid.refresh();
}

function renderBool(bool) {
    if (bool) {
        return "Yes";
    } else {
        return "No";
    }
}

function renderApproved(bool) {
    if (bool) {
        return "Approved";
    } else {
        return "Pending Approval";
    }
}

function changeTableSource(tableName, apiUrl) {
    var grid = document.getElementById(tableName.toString()).ej2_instances[0];
    grid.dataSource = new ej.data.DataManager({
        url: apiUrl,
        adaptor: new ej.data.UrlAdaptor()
    });
}