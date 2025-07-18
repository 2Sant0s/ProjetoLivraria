<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function OnEndCallBack(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoLivros.aspx'
            }
        }
        function OnSalvarLivrosClick(s, e) {
            if (!ASPxClientEdit.ValidateGroup('MyGroup')) {
                e.processOnServer = false;
            } else {
                e.processOnServer = true;
            }
        }
    </script>

    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" Width="100%" Theme="Office365">
    <Items>
        <dx:LayoutItem Caption="Título:" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxTextBox ID="txbCadastroTituloLivro" runat="server" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Digite o título do livro" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Autor:" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxComboBox ID="cbCadastroAutorLivro" runat="server" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Selecione o autor" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Editora:" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxComboBox ID="cbCadastroEditoraLivro" runat="server" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Selecione a editora" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Tipo de Livro:" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxComboBox ID="cbCadastroTipoLivro" runat="server" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Selecione o tipo de livro" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Preço:" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxSpinEdit ID="seCasdastroPrecoLivro" runat="server" MinValue="0" NumberType="Float" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Informe o preço" />
                        </ValidationSettings>
                    </dx:ASPxSpinEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Royalty (%):" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxSpinEdit ID="seCadastroRoyaltyLivro" runat="server" MinValue="0" MaxValue="100" NumberType="Integer" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Informe o royalty" />
                        </ValidationSettings>
                    </dx:ASPxSpinEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Edição:" RequiredMarkDisplayMode="Required">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxSpinEdit ID="seCadastroEdicaoLivro" runat="server" MinValue="1" MaxValue="100" NumberType="Integer" Width="100%">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Informe a edição" />
                        </ValidationSettings>
                    </dx:ASPxSpinEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:LayoutItem Caption="Resumo:" RequiredMarkDisplayMode="Required" ColSpan="2">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxMemo ID="txbCadastroResumoLivro" runat="server" Width="100%" Rows="4">
                        <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                            <RequiredField IsRequired="true" ErrorText="Digite o resumo do livro" />
                        </ValidationSettings>
                    </dx:ASPxMemo>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>

        <dx:EmptyLayoutItem ColSpan="2" />

        <dx:LayoutItem ShowCaption="False" ColSpan="2">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server">
                    <dx:ASPxButton runat="server" Text="Salvar" AutoPostBack="true" ClientSideEvents-Click="OnSalvarLivrosClick" OnClick="btnNovoLivro_Click" Width="100%" />
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>

    <dx:ASPxGridView ID="gvGerenciamentoLivros" runat="server" showInsert="True" AllowEditing="True" Width="100%" KeyFieldName="liv_id_livro"
        OnRowUpdating="gvGerenciamentoLivros_RowUpdating"
        OnRowDeleting="gvGerenciamentoLivros_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoLivros_CustomButtonCallBack">
        <ClientSideEvents EndCallback="OnEndCallBack" />

        <Columns>
            <dx:GridViewDataTextColumn FieldName="liv_id_livro" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="20" FieldName="liv_nm_titulo" Caption="Título" />
            <dx:GridViewDataTextColumn FieldName="liv_vl_preco" Caption="Preço" />
            <dx:GridViewDataTextColumn FieldName="liv_pc_royalty" Caption="Royalty (%)" />
            <dx:GridViewDataTextColumn FieldName="liv_nu_edicao" Caption="Edição" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="100" FieldName="liv_ds_resumo" Caption="Resumo" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="liv_id_editor" Caption="Editora" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="liv_id_tipo_livro" Caption="Tipo de Livro" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="aut_nm_nome" Caption="Autor" />

            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true">
                <CustomButtons>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>

</asp:Content>
