set /p version="Versão: "
git subtree split --prefix="Assets/SO Architecture" --branch upm
git tag version upm
git push origin upm --tags
PAUSE