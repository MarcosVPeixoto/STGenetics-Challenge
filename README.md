# STGenetics Challenge

## 📌 Arquitetura em Camadas

O projeto foi estruturado utilizando **arquitetura em camadas**, com separação clara de responsabilidades entre:

* **Business**: regras de negócio, casos de uso e orquestração
* **Domain**: entidades e regras centrais do domínio
* **Infra**: acesso a dados e integrações externas
* **API**: exposição dos endpoints (por falta de atenção a pasta ficou sem o nome API)
* **Tests**: testes unitários

Essa divisão facilita a manutenção, testabilidade e evolução do sistema.

---

## 📌 CQRS (Command Query Responsibility Segregation)

Foi aplicado o padrão **CQRS**, separando:

* **Commands**: responsáveis por operações de escrita (criação, atualização, remoção)
* **Queries**: responsáveis por consultas de dados

Essa abordagem melhora a organização do código e permite maior escalabilidade e clareza nas responsabilidades.

---

## 📌 Autenticação

O projeto possui estrutura para **autenticação**, porém:

> ⚠️ Como não foi um requisito do desafio, todos os endpoints estão liberados e não exigem token para acesso.

A implementação foi mantida preparada para futura ativação sem grandes alterações estruturais.

---

## 📌 Regra de Descontos

A lógica de descontos foi implementada de forma a:

* Avaliar múltiplas promoções
* Aplicar automaticamente **a melhor promoção elegível para o usuário**

No entanto, após a implementação, foi identificado um ponto de melhoria:

> Atualmente, os descontos são avaliados com base no **ID do item**, o que pode gerar cadastros repetidos para diferentes combinações.

💡 **Melhoria sugerida**:
Avaliar os descontos com base no **tipo do item**, tornando a solução mais flexível e evitando redundância de dados.

---

## 📌 Validações

As validações foram implementadas, porém há uma limitação:

> ⚠️ As mensagens de erro ainda podem ser melhoradas para retornar apenas o conteúdo relevante da validação, sem informações adicionais desnecessárias.

Isso pode ser refinado para melhorar a experiência de consumo da API.

---

## 📌 Testes Unitários

Os testes unitários cobrem os principais cenários de negócio, porém há uma oportunidade clara de melhoria na organização do código de teste.

Atualmente, há repetição na criação de objetos complexos, como descontos e itens de menu (exemplo no arquivo de testes ), o que pode dificultar a manutenção e leitura.

💡 **Melhor prática sugerida**:
Utilizar o padrão **Builder** para construção de objetos nos testes, evitando repetição de código (*copy/paste*) e facilitando a criação de cenários variados.

### Exemplo de melhoria:

* Criar um `DiscountBuilder`
* Criar um `MenuItemBuilder`
* Permitir customização fluente (fluent API)

```csharp
var discount = new DiscountBuilder()
    .WithPercentage(20)
    .WithMenuItem(_xBurguer, 1)
    .WithMenuItem(_batataFrita, 1)
    .WithMenuItem(_refrigerante, 1)
    .Build();
```

✅ Benefícios:

* Redução de duplicação de código
* Testes mais legíveis
* Facilidade para alterar regras futuras
* Centralização da criação de objetos de teste

---

## 🚀 Como executar o projeto

Para rodar o projeto localmente, execute o seguinte comando na pasta:

```
STGenetics.Challenge\STGenetics.Challenge
```

```bash
dotnet run
```

---

## ✅ Considerações finais

O projeto atende aos requisitos principais do desafio, com foco em boas práticas de arquitetura e organização de código. Alguns pontos foram identificados para melhoria futura, principalmente em relação à modelagem de descontos, refinamento das mensagens de validação e organização dos testes unitários.
