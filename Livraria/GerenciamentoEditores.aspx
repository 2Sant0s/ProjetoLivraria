<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function OnEndCallBack(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoLivros.aspx'
            }
        }
        function OnSalvarEditorClick(s, e) {
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
                    <dx:LayoutItem Caption="Nome">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txbCadastroNomeEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o nome do Editor" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <Items>
                    <dx:LayoutItem Caption="Email">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txbCadastroEmailEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o email do Editor" />
                                        <RegularExpression ErrorText="Email inválido" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="URL" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txbCadastroURLEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o URL do Editor" />
                                        <RegularExpression ErrorText="URL inválido" ValidationExpression="^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton runat="server" Text="Salvar" AutoPostBack="true" CausesValidation="true" Width="100%" OnClick="BtnNovoEditor_Click" ClientSideEvents-Click="OnSalvarEditorClick" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>


    <dx:ASPxGridView ID="gvGerenciamentoEditores" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="edi_id_editor"
        OnRowUpdating="gvGerenciamentoEditores_RowUpdating"
        OnRowDeleting="gvGerenciamentoEditores_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoEditores_CustomButtonCallBack">
        <ClientSideEvents EndCallback="OnEndCallBack" />

        <Columns>
            <dx:GridViewDataTextColumn FieldName="edi_id_editor" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="edi_nm_editor" Caption="Nome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="edi_ds_email" Caption="Email" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="edi_ds_url" Caption="URL" />

            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros" />
                </CustomButtons>
            </dx:GridViewCommandColumn>

        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>
