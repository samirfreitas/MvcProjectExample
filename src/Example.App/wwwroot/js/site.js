function AjaxModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                    keyboard: true
                                },
                                'show');
                            bindForm(this);
                        }
                    );
                    return false;
                }
            );
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#AddressTarget').load(result.url);

                        } else {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;

            });
        }

    });

};


function SearchCep() {
    $(document).ready(function () {

        function clear_form_cep() {
            $('#Address_PublicPlace').val('');
            $('#Address_District').val('');
            $('#Address_City').val('');
            $('#Address_State').val('');
        }

        $('#Address_ZipCode').blur(function () {
        
            var zipCode = $(this).val().replace(/\D/g, '');

            if (zipCode != "") {
                var validateZipCode = /^[0-9]{8}$/;

                if (validateZipCode.test(zipCode)) {

                    $('#Address_PublicPlace').val('...');
                    $('#Address_District').val('...');
                    $('#Address_City').val('...');
                    $('#Address_State').val('...');


                    $.getJSON("https://viacep.com.br/ws/" + zipCode + "/json/?callback=?", function (dados) {

                        if (!("erro" in dados)) {

                            $('#Address_PublicPlace').val(dados.logradouro);
                            $('#Address_District').val(dados.bairro);
                            $('#Address_City').val(dados.localidade);
                            $('#Address_State').val(dados.uf);

                        } else {
                            clear_form_cep();
                            Alert('CEP nao encontrado.')
                        }

                    });

                }
            } else {
                clear_form_cep();
                Alert('Formato de CEP invalido.')
            }
        });

    });
};