<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function OnEndCallBack(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoLivros.aspx'
            }
        }
        function OnSalvarCategoriaClick(s, e) {
            if (!ASPxClientEdit.ValidateGroup('MyGroup')) {
                e.processOnServer = false;
            } else {
                e.processOnServer = true;
            }
        }
    </script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <dx:LayoutItem Caption="Categoria">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txbCadastroCategoria" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite a categoria do livro" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <Items>

                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton runat="server" Text="Salvar" AutoPostBack="true" CausesValidation="true" Width="100%" OnClick="BtnNovaCategoria_Click" ClientSideEvents-Click="OnSalvarCategoriaClick" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    
    <dx:ASPxGridView ID="gvGerenciamentoCategoria" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="til_id_tipo_livro"
        OnRowUpdating="gvGerenciamentoCategoria_RowUpdating"
        OnRowDeleting="gvGerenciamentoCategoria_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoCategoria_CustomButtonCallBack">
        <ClientSideEvents EndCallback="OnEndCallBack" />

        <Columns>
            <dx:GridViewDataTextColumn FieldName="til_ds_descricao" Caption="Categoria" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="til_ds_descricao" Caption="Categoria" />
            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true">
               
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros" />
                </CustomButtons>
            </dx:GridViewCommandColumn>

        </Columns>

       <%-- <SettingsEditing Mode="Batch" /> INVESTIGAR O PORQUÊ NÃO FUNCIONA COM ESSE MODO. --%> 

        <SettingsEditing Mode="EditForm" />
    </dx:ASPxGridView>
</asp:Content>
