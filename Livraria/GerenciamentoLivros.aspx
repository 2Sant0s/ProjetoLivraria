<%@ Page Title="Gerenciamento de Livros" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script> 
        function OnEndCallback(s, e) {

        }
        function OnSalvarLivroClick(s, e) {
            if (!ASPxClientEdit.ValidateGroup('MyGroup')) {
                e.processOnServer = false;
            } else {
                e.processOnServer = true;
            }
        }
    </script>

    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="" ColumnCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <dx:LayoutItem Caption="Titulo" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroTituloLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o título do Livro!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Autor">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cbCadastroAutorLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Selecione o Autor" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Categoria">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cbCadastroCategoria" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Selecione a Categoria" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Editor">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cbCadastroEditorLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Selecione o Editor" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Preço">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="seCasdastroPrecoLivro" runat="server" MinValue="0" MaxValue="9999" NumberType="Float" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Informe o preço" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Royalty (%)">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="seCadastroRoyaltyLivro" runat="server" MinValue="0" MaxValue="9999" NumberType="Float" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Informe o royalty" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Edição">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
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

                    <dx:LayoutItem Caption="Resumo" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo ID="tbxCadastroResumoLivro" runat="server" Width="100%" Rows="4">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o resumo do livro" />
                                    </ValidationSettings>
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="False" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton runat="server" Text="Salvar" AutoPostBack="false" ValidationGroup="MyGroup"
                                    Width="100%"
                                    UseSubmitBehavior="false"
                                    OnClick="BtnNovoLivro_Click"
                                    ClientSideEvents-Click="OnSalvarLivroClick" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxGridView ID="gvGerenciamentoLivros" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="liv_id_livro" 
        OnRowUpdating="gvGerenciamentoLivros_RowUpdating"
        OnRowDeleting="gvGerenciamentoLivros_RowDeleting">
        <ClientSideEvents EndCallback="OnEndCallback" />
          <SettingsSearchPanel Visible="False" />
        <Settings ShowFilterRow="False" />

        <Columns>
            <dx:GridViewDataTextColumn FieldName="liv_id_livro" Caption="ID" Visible="false" />

            <dx:GridViewDataTextColumn FieldName="liv_nm_titulo" Caption="Título" />
            <dx:GridViewDataTextColumn FieldName="liv_vl_preco" Caption="Preço" />
            <dx:GridViewDataTextColumn FieldName="liv_nu_edicao" Caption="Edição" />
            <dx:GridViewDataTextColumn FieldName="liv_pc_royalty" Caption="Royalty (%)" />
            <dx:GridViewDataTextColumn FieldName="liv_ds_resumo" Caption="Resumo" />

            <dx:GridViewDataTextColumn FieldName="aut_nm_nome" Caption="Autor" ReadOnly="True">
                <EditItemTemplate>
                    <dx:ASPxComboBox ID="cbAutorEdit" runat="server"
                        DataSourceID="AUT_AUTORES"
                        TextField="aut_nm_nome"
                        ValueField="aut_id_autor"
                        Value='<%# Bind("aut_id_autor") %>'>
                    </dx:ASPxComboBox>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>

            <%-- ** editar um campo de uma GridView utilizando combobox que puxa dados do bd ** --%>
            <dx:GridViewDataTextColumn FieldName="edi_nm_nome" Caption="Editor">
                <EditItemTemplate>
                    <dx:ASPxComboBox ID="cbEditor" runat="server"
                        DataSourceID="EDI_EDITORES"
                        ValueField="edi_id_editor"
                        TextField="EDI_NM_EDITOR"
                        Value='<%# Bind("edi_id_editor") %>'>
                    </dx:ASPxComboBox>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="til_ds_descricao" Caption="Categoria" ReadOnly="True">
                <EditItemTemplate>
                    <dx:ASPxComboBox ID="cbCategoriaEdit" runat="server"
                        DataSourceID="TIL_TIPO_LIVRO"
                        ValueField="til_id_tipo_livro"
                        TextField="TIL_DS_DESCRICAO"
                        Value='<%# Bind("til_tipo_livro") %>'>
                    </dx:ASPxComboBox>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>

        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>

    <%-- comboBox do gridview puxa dados daqui --%>
    <asp:SqlDataSource ID="AUT_AUTORES" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT AUT_ID_AUTOR, AUT_NM_NOME FROM AUT_AUTORES" />

    <asp:SqlDataSource ID="EDI_EDITORES" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT EDI_ID_EDITOR, EDI_NM_EDITOR FROM EDI_EDITORES" />

    <asp:SqlDataSource ID="TIL_TIPO_LIVRO" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT til_id_tipo_livro, til_ds_descricao FROM TIL_TIPO_LIVRO" />
</asp:Content>
