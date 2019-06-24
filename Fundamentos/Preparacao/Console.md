# Console de Desenvolvimento

Todo e qualquer código está sujeito a erros, principalmente se ele é gerado por humanos. Quando você estiver codando em JS, esses erros nem sempre irão aparecer na página. E se não podemos visualizá-lo, é difícil de consertá-lo.

Para isso, foi criado o console nos navegadores, a fim de permitir a melhor visualização de logs/erros pelo desenvolvedor e também pelo usuário avançado.

Aqui, cobriremos algumas dicas de como abrir o console de várias maneiras, além de verificar o que normalmente é impresso.

## Abrir o Console

Acesse o Console como um painel dedicado em tela inteira:

![O painel do Console](https://developers.google.com/web/tools/chrome-devtools/console/images/console-panel.png?hl=pt-br)

Ou como uma gaveta que é aberta ao lado de qualquer painel:

![A gaveta do Console](https://developers.google.com/web/tools/chrome-devtools/console/images/console-drawer.png?hl=pt-br)

### Abrir como um painel

Para abrir o painel dedicado **Console**:

- Pressione Ctrl+Shift+J (Windows/Linux) ou Cmd+Opt+J (Mac).
- Se o DevTools já estiver aberto, pressione o botão do **Console**.

Quando abrir o painel do Console, a gaveta do Console será recolhida automaticamente.

### Abrir como uma gaveta

Para abrir o Console como uma gaveta próxima a qualquer outro painel:

- Pressione Esc com o DevTools em foco.
- Pressione o botão **Customize and control DevTools** e **Show console**.

![Mostrar console](https://developers.google.com/web/tools/chrome-devtools/console/images/show-console.png?hl=pt-br)

## Empilhamento de mensagens

Se uma mensagem for repetida consecutivamente, em vez de gerar cada instância da mensagem em uma nova linha, o Console "empilha" as mensagens e exibe um número na margem esquerda. O número indica quantas vezes a mensagem foi repetida.

![Empilhamento de mensagens](https://developers.google.com/web/tools/chrome-devtools/console/images/message-stacking.png?hl=pt-br)

Caso prefira uma entrada de linha exclusiva para cada registro, ative **Show timestamps** nas configurações do DevTools.

![Mostrar carimbos de data/hora](https://developers.google.com/web/tools/chrome-devtools/console/images/show-timestamps.png?hl=pt-br)

Como o carimbo de data/hora de cada mensagem é diferente, cada uma delas é exibida na própria linha.

![Console com carimbo de data/hora](https://developers.google.com/web/tools/chrome-devtools/console/images/timestamped-console.png?hl=pt-br)

## Trabalhar com o histórico do Console

### Apagar o histórico

Você pode apagar o histórico do console seguindo qualquer um destes procedimentos:

- Clique com o botão direito no Console e pressione **Clear console**.
- Digite `clear()` no Console.
- Chame `console.clear()` a partir do código JavaScript.
- Digite Ctrl+L (Mac, Windows, Linux).

### Manter o histórico

Marque a caixa de seleção **Preserve log** na parte superior do console para manter o histórico ao fazer atualizações ou mudanças na página. As mensagens serão armazenadas até que você apague o Console ou feche a guia.

### Salvar o histórico

Clique com o botão direito no Console e selecione **Save as** para salvar a saída do console em um arquivo de registros.

![Salvar Console em um arquivo de registros](https://developers.google.com/web/tools/chrome-devtools/console/images/console-save-as.png?hl=pt-br)

## Selecionar contexto de execução

O menu suspenso destacado em azul na captura de tela abaixo é chamado de **Execution Context Selector**.

![Execution Context Selector](https://developers.google.com/web/tools/chrome-devtools/console/images/execution-context-selector.png?hl=pt-br)

Geralmente, o contexto é definido como `top` (o frame superior da página).

Outros frames e extensões são operados nos próprios contextos. Para trabalhar com esses outros contextos, você precisa selecioná-los no menu suspenso. Por exemplo, se quiser ver a saída de registro de um elemento `<iframe>` e modificar uma variável que existe nesse contexto, você deve selecioná-lo no menu suspenso Execution Context Selector.

O contexto padrão do Console é `top`, a não ser que você acesse o DevTools inspecionando um elemento em outro contexto. Por exemplo, se você inspecionar um elemento `<p>` em um `<iframe>`, o DevTools definirá o Execution Context Selector para o contexto desse `<iframe>`.

Ao trabalhar em um contexto diferente de `top`, o DevTools destaca o Execution Context Selector em vermelho, como na captura de tela abaixo. Isso ocorre porque os desenvolvedores raramente precisam trabalhar em contextos diferentes de `top`. Pode ser bastante confuso digitar uma variável, esperando um valor, apenas para ver que esse valor é `undefined`(porque ele foi definido para um contexto diferente).

![Execution Context Selector destacado em vermelho](https://developers.google.com/web/tools/chrome-devtools/console/images/non-top-context.png?hl=pt-br)

## Filtrar a saída do Console

Clique no botão **Filter** (![botão filtrar](https://developers.google.com/web/tools/chrome-devtools/console/images/filter-button.png?hl=pt-br)) para filtrar a saída do console. Você pode filtrar por nível de gravidade, por uma expressão regular ou ocultando mensagens de rede.

![Saída do Console filtrada](https://developers.google.com/web/tools/chrome-devtools/console/images/filtered-console.png?hl=pt-br)

Filtrar por nível de gravidade é equivalente ao seguinte:

| Opção & O que ela mostra |                                                              |
| :----------------------- | ------------------------------------------------------------ |
| All                      | Mostra toda a saída do console                               |
| Errors                   | Mostra apenas a saída de [console.error()](https://developers.google.com/web/tools/chrome-devtools/debug/console/console-reference?hl=pt-br#consoleerrorobject--object-). |
| Warnings                 | Mostra apenas a saída de [console.warn()](https://developers.google.com/web/tools/chrome-devtools/debug/console/console-reference?hl=pt-br#consolewarnobject--object-). |
| Info                     | Mostra apenas a saída de [console.info()](https://developers.google.com/web/tools/chrome-devtools/debug/console/console-reference?hl=pt-br#consoleinfoobject--object-). |
| Logs                     | Mostra apenas a saída de [console.log()](https://developers.google.com/web/tools/chrome-devtools/debug/console/console-reference?hl=pt-br#consolelogobject--object-). |
| Depurar                  | Mostra apenas a saída de [console.timeEnd()](https://developers.google.com/web/tools/chrome-devtools/debug/console/console-reference?hl=pt-br#consoletimeendlabel) e [console.debug()](https://developers.google.com/web/tools/chrome-devtools/debug/console/console-reference?hl=pt-br#consoledebugobject--object-). |

## Configurações adicionais

Abra as configurações do DevTools, acesse a guia General e role a página até a seção **Console** para definir configurações adicionais do Console.

![Configurações do Console](https://developers.google.com/web/tools/chrome-devtools/console/images/console-settings.png?hl=pt-br)

| Configuração & Descrição     |                                                              |
| :--------------------------- | ------------------------------------------------------------ |
| Hide network messages        | Por padrão, o console reporta problemas na rede. Ativar essa opção instrui o console a não exibir registros para esses erros. Por exemplo, erros das séries 404 e 500 não serão registrados. |
| Log XMLHttpRequests          | Determina se o console registra cada XMLHttpRequest.         |
| Preserve log upon navigation | Mantém o histórico do console durante atualizações de página ou navegação. |
| Show timestamps              | Adiciona um carimbo de data/hora ao início de cada mensagem do console exibida quando a chamada é feita. Útil para depurar quando determinado evento ocorre. Isso desativará o empilhamento de mensagens. |
| Enable custom formatters     | Controla a [formatação](https://docs.google.com/document/d/1FTascZXT9cxfetuPRT2eXPQKXui4nWFivUnS_335T3U/preview?hl=pt-br) de objetos JavaScript. |