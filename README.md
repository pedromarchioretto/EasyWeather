# EasyWeather 

Este é um aplicativo mobile feito em Xamarin para mostrar dados de tempo e clima de qualquer cidade do mundo. Exclusivo para dispositivos `Android`!

## Informações disponibilizadas
- País em que a cidade pesquisada se localiza

- Temperatura atual em `C°`

- Temperatura máxima e mínima em `C°`

- Descrição do clima: `nublado`, `ensolarado`, `chovendo`, `nevando` e entre outras

- Umidade do local em `%` 

- Velocidade do vento em `Km/h`

- Visibilidade na cidade em `metros`

## Features
- Você terá a opção de favoritar ou desfavoritar a cidade, acelerando o processo caso queira pesquisá-la novamente ⭐

- Ao clicar na lupa de pesquisa, será possível visualizar o campo `Cidades favoritas`, clique nele e você verá todas as cidades salvas. Escolha a cidade favoritada de sua preferência e será levado diretamente para visualizar seu tempo e clima.

## User Experience e User Interface

- As informações são distribuídas na tela para facilitar `ao máximo` a visualização

- Conforme a descrição do clima, o background mudará para imagens ilustrativas (`neve`, `sol` . . . ), melhorando a experiência do usuário

- O aplicativo possui seu layout mais `arredondado`, dando sensação de fluidez e minimalismo, além de possui uma fonte de facil leitura
  
  
## Como executar
- `Observação: não esqueça de baixar o Microsoft Visual Studio previamente:` [Clique aqui para baixar a versão gratuita](https://visualstudio.microsoft.com/pt-br/downloads/)

---

### 1: Faça o clone do repositório no seu computador

- Crie uma pasta ou entre em uma existente para fazer o clone

~~~ 
    git clone <URL copiada>
~~~

Entre na pasta clonada: `\EasyWeather_Solution\` e abra o arquivo `EasyWeather_Solution.sln`

### 2: Mude as API keys

- Entre no site da [OpenWeather](https://openweathermap.org/api) e `crie` sua conta `gratuitamente`
- Entre na página de [API keys](https://home.openweathermap.org/api_keys) e aperte para `gerar` sua `APIkey` da OpenWeather
- Realize o mesmo processo para o site [TimeZoneDB](https://timezonedb.com/api) e pegue `APIkey`
- Dentro do código, procure pela classe `ChavesAPI.cs` e troque os valores:
~~~
    public string OpenWeatherAPI = "<Sua APIkey da OpenWeather>";
    public string TimeZoneAPI = "<Sua APIkey da TimeZoneDB>";
~~~

### 3: Execução

Você pode escolher executar o aplicativo em um emulador (`No próprio visual studio`) ou executar diretamente no seu celular. Para isso:

1. Entre nas `Configurações` do seu celular.

2. Vá para `Sobre o telefone`.

3. Toque em `Informações do software` e clique `7 vezes` em cima da opção `Versão do Android`.

4. Volte para as `Configurações` do seu celular e role até encontrar a opção `Opções de Desenvolvedor`. 

5. Por fim, procure pela opção `Depuração USB` e ative essa opção.

6. Retorne ao `Visual Studio` e conecte seu celular por `USB` no computador. Ao lado do botão de execução do código, no topo da tela, você poderá ver o nome do seu dispositivo, `clique para executar` para iniciar a compilação (`isso pode demorar`) e aguarde o aplicativo se iniciar :) Aproveite !







