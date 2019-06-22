function formatDate(data) {
    var date = new Date(data);
    return moment(date).format("DD MMM YYYY");
}