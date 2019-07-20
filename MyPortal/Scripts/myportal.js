function formatDate(data) {
    var date = new Date(data);
    return moment(date).format("DD MMM YYYY");
}

function formatCurrency(data) {
    return "£" + data.toFixed(2).toString();
}