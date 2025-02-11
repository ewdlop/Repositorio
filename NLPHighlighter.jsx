import React, { useState } from 'react';
import { Card, CardContent } from '@/components/ui/card';
import { FileText, GitBranch, Network } from 'lucide-react';

const NLPHighlighter = () => {
  const [text, setText] = useState("The quick brown fox jumps over the lazy dog in New York.");
  const [viewMode, setViewMode] = useState('pos'); // pos, deps, ner, tree

  // Enhanced POS tagger with more categories
  const posStyles = {
    noun: 'bg-blue-100',
    verb: 'bg-green-100',
    adjective: 'bg-yellow-100',
    adverb: 'bg-purple-100',
    determiner: 'bg-gray-100',
    preposition: 'bg-pink-100',
    pronoun: 'bg-orange-100',
    conjunction: 'bg-teal-100',
    proper_noun: 'bg-red-100',
  };

  // Named Entity types and styles
  const nerStyles = {
    person: 'bg-red-200 border-red-400',
    location: 'bg-green-200 border-green-400',
    organization: 'bg-blue-200 border-blue-400',
    date: 'bg-yellow-200 border-yellow-400',
  };

  // Dependency relations
  const depRelations = {
    subject: 'text-blue-600',
    object: 'text-green-600',
    modifier: 'text-purple-600',
    root: 'text-red-600',
  };

  // Enhanced POS tagger
  const tagPOS = (word) => {
    const rules = {
      the: 'determiner',
      a: 'determiner',
      an: 'determiner',
      quick: 'adjective',
      brown: 'adjective',
      lazy: 'adjective',
      fox: 'noun',
      dog: 'noun',
      jumps: 'verb',
      jumped: 'verb',
      over: 'preposition',
      in: 'preposition',
      and: 'conjunction',
      but: 'conjunction',
      he: 'pronoun',
      she: 'pronoun',
      quickly: 'adverb',
      slowly: 'adverb',
    };
    
    // Check for proper nouns (basic implementation)
    if (word.match(/^[A-Z][a-z]+$/)) {
      return 'proper_noun';
    }
    
    return rules[word.toLowerCase()] || 'unknown';
  };

  // Named Entity Recognition (basic implementation)
  const recognizeEntities = (word) => {
    const entities = {
      'New York': 'location',
      'John': 'person',
      'Google': 'organization',
      'Monday': 'date',
    };
    
    return entities[word] || null;
  };

  // Dependency Parser (basic implementation)
  const parseDependencies = (tokens) => {
    const deps = [];
    let root = null;
    
    // Find the main verb as root
    const verbIndex = tokens.findIndex(t => t.pos === 'verb');
    if (verbIndex !== -1) {
      root = verbIndex;
      
      // Find subject (noun before verb)
      const subjectIndex = tokens.findIndex((t, i) => i < verbIndex && (t.pos === 'noun' || t.pos === 'proper_noun'));
      if (subjectIndex !== -1) {
        deps.push({
          from: verbIndex,
          to: subjectIndex,
          type: 'subject'
        });
      }
      
      // Find object (noun after verb)
      const objectIndex = tokens.findIndex((t, i) => i > verbIndex && (t.pos === 'noun' || t.pos === 'proper_noun'));
      if (objectIndex !== -1) {
        deps.push({
          from: verbIndex,
          to: objectIndex,
          type: 'object'
        });
      }
      
      // Find modifiers
      tokens.forEach((token, i) => {
        if (token.pos === 'adjective' || token.pos === 'adverb') {
          const targetIndex = i + 1;
          if (targetIndex < tokens.length) {
            deps.push({
              from: targetIndex,
              to: i,
              type: 'modifier'
            });
          }
        }
      });
    }
    
    return { root, deps };
  };

  // Process text with all analysis
  const processText = (inputText) => {
    const tokens = inputText.split(/\s+/).map((word, index) => ({
      word,
      pos: tagPOS(word),
      ner: recognizeEntities(word),
      id: index,
    }));
    
    const { root, deps } = parseDependencies(tokens);
    
    return { tokens, root, deps };
  };

  const { tokens, root, deps } = processText(text);

  // Tree Node Component
  const TreeNode = ({ node, level = 0 }) => {
    if (!node) return null;
    
    return (
      <div className="ml-6">
        <div className="flex items-center">
          <span className="font-mono">{node.label}</span>
          {node.word && <span className="ml-2 text-gray-600">({node.word})</span>}
        </div>
        {node.children && (
          <div className="border-l-2 border-gray-200 ml-2">
            {node.children.map((child, i) => (
              <TreeNode key={i} node={child} level={level + 1} />
            ))}
          </div>
        )}
      </div>
    );
  };

  return (
    <Card className="w-full max-w-4xl">
      <CardContent className="p-6">
        <div className="mb-4">
          <textarea
            value={text}
            onChange={(e) => setText(e.target.value)}
            className="w-full p-2 border rounded"
            rows={3}
          />
        </div>
        
        <div className="mb-4 flex gap-2">
          <button
            onClick={() => setViewMode('pos')}
            className={`flex items-center gap-1 px-3 py-1 rounded ${viewMode === 'pos' ? 'bg-blue-100' : 'bg-gray-100'}`}
          >
            <FileText size={16} /> POS Tags
          </button>
          <button
            onClick={() => setViewMode('deps')}
            className={`flex items-center gap-1 px-3 py-1 rounded ${viewMode === 'deps' ? 'bg-blue-100' : 'bg-gray-100'}`}
          >
            <Network size={16} /> Dependencies
          </button>
          <button
            onClick={() => setViewMode('ner')}
            className={`flex items-center gap-1 px-3 py-1 rounded ${viewMode === 'ner' ? 'bg-blue-100' : 'bg-gray-100'}`}
          >
            <GitBranch size={16} /> NER
          </button>
        </div>

        {viewMode === 'pos' && (
          <>
            <div className="flex flex-wrap gap-2 mb-6">
              {tokens.map((token) => (
                <div
                  key={token.id}
                  className={`relative group cursor-help p-1 rounded ${posStyles[token.pos] || 'bg-gray-50'}`}
                >
                  {token.word}
                  <div className="invisible group-hover:visible absolute -top-8 left-1/2 transform -translate-x-1/2 bg-gray-800 text-white px-2 py-1 rounded text-sm">
                    {token.pos || 'unknown'}
                  </div>
                </div>
              ))}
            </div>
            
            <div className="flex flex-wrap gap-2">
              {Object.entries(posStyles).map(([pos, style]) => (
                <div key={pos} className="flex items-center gap-1">
                  <div className={`w-4 h-4 ${style} rounded`}></div>
                  <span className="text-sm capitalize">{pos.replace('_', ' ')}</span>
                </div>
              ))}
            </div>
          </>
        )}

        {viewMode === 'deps' && (
          <div className="relative min-h-64 mt-4">
            <div className="flex flex-wrap gap-2">
              {tokens.map((token, i) => (
                <div
                  key={token.id}
                  className={`relative p-1 rounded ${i === root ? 'bg-red-100' : 'bg-gray-50'}`}
                >
                  {token.word}
                  {deps.map((dep, j) => {
                    if (dep.from === i || dep.to === i) {
                      return (
                        <div
                          key={j}
                          className={`absolute w-24 h-px ${depRelations[dep.type]} transform rotate-45`}
                          style={{
                            top: dep.from === i ? '100%' : '0',
                            left: '50%',
                          }}
                        />
                      );
                    }
                    return null;
                  })}
                </div>
              ))}
            </div>
            
            <div className="mt-8 flex flex-wrap gap-2">
              {Object.entries(depRelations).map(([rel, style]) => (
                <div key={rel} className="flex items-center gap-1">
                  <div className={`w-4 h-px ${style}`}></div>
                  <span className="text-sm capitalize">{rel}</span>
                </div>
              ))}
            </div>
          </div>
        )}

        {viewMode === 'ner' && (
          <>
            <div className="flex flex-wrap gap-2 mb-6">
              {tokens.map((token) => (
                <div
                  key={token.id}
                  className={`relative group cursor-help p-1 rounded border ${
                    token.ner ? nerStyles[token.ner] : 'bg-gray-50'
                  }`}
                >
                  {token.word}
                  {token.ner && (
                    <div className="invisible group-hover:visible absolute -top-8 left-1/2 transform -translate-x-1/2 bg-gray-800 text-white px-2 py-1 rounded text-sm">
                      {token.ner}
                    </div>
                  )}
                </div>
              ))}
            </div>
            
            <div className="flex flex-wrap gap-2">
              {Object.entries(nerStyles).map(([ner, style]) => (
                <div key={ner} className="flex items-center gap-1">
                  <div className={`w-4 h-4 ${style} rounded`}></div>
                  <span className="text-sm capitalize">{ner}</span>
                </div>
              ))}
            </div>
          </>
        )}
      </CardContent>
    </Card>
  );
};
export default NLPHighlighter;
