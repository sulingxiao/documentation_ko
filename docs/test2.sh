sed "/---/, /---/d" ./pages/doc_en/ontology_ddxf_en.md > tmp
echo "---
sidebar : ont_doc_en
permalink : ontology_ddxf_en.html
folder : doc_ko
---" | cat - tmp > temp
sed -E "s/^(\[English|English|\[한국어|한국어).*$/\[English\](\.\/ontology_ddxf_ko.html) \/ 한국어 /g" temp > tmp && cat tmp >temp
cat temp > ./pages/doc_en/ontology_ddxf_en.md
