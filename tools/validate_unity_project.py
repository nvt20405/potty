#!/usr/bin/env python3
from pathlib import Path
import argparse, json, re, sys

def load_json(p):
    with p.open(encoding='utf-8-sig') as f: return json.load(f)

def main(project: Path):
    assets=project/'Assets'; res=assets/'Resources'; cfg=res/'config'
    issues=[]; warnings=[]
    guid_to_meta={}; dup_guids=[]
    for m in assets.rglob('*.meta'):
        try: txt=m.read_text(errors='ignore')[:4096]
        except: continue
        mt=re.search(r'(?m)^guid:\s*([0-9a-fA-F]{32})\s*$',txt)
        if not mt: continue
        g=mt.group(1).lower()
        if g in guid_to_meta and guid_to_meta[g] != m: dup_guids.append((g,guid_to_meta[g],m))
        else: guid_to_meta[g]=m
    if dup_guids: issues.append(f'duplicate meta GUIDs: {len(dup_guids)}')

    refs=set(); ref_files=0
    yaml_ext={'.prefab','.unity','.mat','.anim','.asset','.controller','.overridecontroller','.playable','.mask'}
    for p in assets.rglob('*'):
        if not p.is_file() or p.suffix.lower() not in yaml_ext: continue
        try: text=p.read_text(errors='ignore')
        except: continue
        ref_files+=1
        refs.update(g.lower() for g in re.findall(r'guid:\s*([0-9a-fA-F]{32})',text))
    unresolved=sorted(g for g in refs if g not in guid_to_meta and not g.startswith('0000000000000000'))
    if unresolved: issues.append(f'unresolved project GUID refs: {len(unresolved)}')

    seen={}; collisions=[]
    for p in assets.rglob('*'):
        if not p.is_file(): continue
        rel=p.relative_to(project).as_posix(); k=rel.casefold()
        if k in seen and seen[k] != rel: collisions.append((seen[k],rel))
        else: seen[k]=rel
    if collisions: warnings.append(f'case-insensitive file collisions: {len(collisions)}')

    checks=[]
    def check_keys(json_name, folder, selector=lambda k: True):
        data=load_json(cfg/json_name); expected={k for k in data if selector(k)}
        actual={p.stem for p in (res/folder).glob('*.prefab')}
        missing=sorted(expected-actual); extra=sorted(actual-expected)
        checks.append((json_name,folder,len(expected),len(actual),missing,extra))
        return missing
    missing_nv=check_keys('NhanVat.json','nhanvat')
    missing_costume=check_keys('Costume.json','costumes')
    missing_weapon=check_keys('TrangBi.json','vukhi',lambda k:k.startswith('VK_'))
    if missing_nv: issues.append(f'character model prefabs missing: {len(missing_nv)}')
    if missing_costume: issues.append(f'costume model prefabs missing: {len(missing_costume)}')
    if missing_weapon: warnings.append(f'weapon config entries using hidden/default fallback rather than same-name prefab: {len(missing_weapon)}')

    resource_paths=set(); resource_exact={}
    for p in res.rglob('*'):
        if p.is_file() and p.suffix!='.meta':
            rel=p.relative_to(res).as_posix(); noext=str(Path(rel).with_suffix(''))
            resource_paths.add(noext.casefold()); resource_exact[noext.casefold()] = noext
    literals=[]; missing_literals=[]; case_mismatch_literals=[]
    rx=re.compile(r'(?:Resources\.Load(?:<[^>]+>)?|EGResourceAsyncLoader\.Load)\(\s*"([^"]+)"')
    for p in assets.rglob('*.cs'):
        text=p.read_text(errors='ignore')
        for m in rx.finditer(text):
            val=m.group(1)
            after=text[m.end():m.end()+32]
            if val.endswith('/') or val.endswith('_') or re.match(r'\s*\+', after):
                continue
            literals.append((p,val))
            key=val.casefold()
            if key in resource_exact and resource_exact[key] != val:
                case_mismatch_literals.append((str(p.relative_to(project)),val,resource_exact[key]))
            if key not in resource_paths and not any(x.startswith(key+'/') for x in resource_paths):
                missing_literals.append((str(p.relative_to(project)),val))
    if case_mismatch_literals: issues.append(f'literal Resources path case mismatches: {len(case_mismatch_literals)}')
    if missing_literals: warnings.append(f'literal Resources paths not found: {len(missing_literals)}')

    print('=== UNITY PROJECT VALIDATION ===')
    print('project:',project)
    print('meta GUIDs:',len(guid_to_meta),'serialized files checked:',ref_files,'referenced GUIDs:',len(refs),'unresolved:',len(unresolved))
    for name,folder,ne,na,missing,extra in checks:
        print(f'{name}: expected={ne} local_prefabs={na} missing={len(missing)} extra={len(extra)}')
        if missing: print('  missing:',', '.join(missing[:30]))
    print('literal resource loads:',len(literals),'case_mismatches:',len(case_mismatch_literals),'missing:',len(missing_literals))
    if case_mismatch_literals:
        for f,got,want in case_mismatch_literals[:40]: print('  resource-case:',f,'=>',got,'expected',want)
    if missing_literals:
        for f,v in missing_literals[:40]: print('  resource-miss:',f,'=>',v)
    if collisions:
        for a,b in collisions[:20]: print('  case-collision:',a,'<=>',b)
    for w in warnings: print('WARNING:',w)
    for e in issues: print('ERROR:',e)
    return 1 if issues else 0

if __name__=='__main__':
    ap=argparse.ArgumentParser(); ap.add_argument('project',type=Path)
    sys.exit(main(ap.parse_args().project.resolve()))
