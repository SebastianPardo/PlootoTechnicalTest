var Utilities = /** @class */ (function () {
    function Utilities() {
        this.manageRequest = function (options) {
            $.ajax({
                url: 'https://localhost:44303/' + options.url,
                data: options.data,
                type: options.type,
                success: function (response, status, jqXhr) {
                    if (options.callback != undefined) {
                        options.callback(response);
                    }
                },
                error: function (jqXhr, status, error) {
                    if (options.errorMessage === undefined || options.errorMessage === null || options.errorMessage === '') {
                        swal.fire('Error', 'No se pudo obtener respuesta satisfactoria debido al error: ' + error, 'error');
                    }
                    else {
                        swal.fire('Error', options.errorMessage, 'error');
                    }
                }
            });
        };
    }
    return Utilities;
}());
//# sourceMappingURL=Utilities.js.map